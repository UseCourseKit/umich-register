using System.Text.Json;

class APIClient
{
    private readonly HttpClient client = new HttpClient();
    private readonly object headerLock = new();
    private string? AccessToken;
    private DateTime AccessTokenExpirationTime;
    // When rate restricted, when do we resume requests?
    private DateTime? NextRequestTime = null;
    private DateTime SnapshotTime = DateTime.Now;

    public APIClient()
    {
        client.DefaultRequestHeaders.Accept.Add(new("application/json"));
    }

    public async Task<IEnumerable<CourseSection>> ReadSections(string term, string subject, short catalogNumber)
    {
        while (true)
        {
            if (NextRequestTime != null && NextRequestTime > DateTime.Now)
            {
                await Task.Delay((int) Math.Ceiling(NextRequestTime.Value.Subtract(DateTime.Now).TotalMilliseconds));
            }
            try
            {
                // await EnsureRefreshedTokenLoaded();
                var res = await client.GetStreamAsync($"https://gw.api.it.umich.edu/um/Curriculum/SOC/Terms/{term}/Schools/UM/Subjects/{subject}/CatalogNbrs/{catalogNumber}/Sections?IncludeAllSections=Y");
                var resJson = await JsonDocument.ParseAsync(res);
                // Uniform timing so that aggregated queries can group by time
                // Each group with the same time should be guaranteed to contain all the sections for a given class at that time
                var now = DateTime.Now;
                Func<JsonElement, CourseSection> elemToSection = (elem) => new CourseSection
                {
                    ClassNumber = (uint) JsonFieldToInt(elem.GetProperty("ClassNumber")),
                    CourseCode = $"{subject} {catalogNumber}",
                    NumCapacity = (ushort) JsonFieldToInt(elem.GetProperty("EnrollmentCapacity")),
                    NumOpen = (ushort) JsonFieldToInt(elem.GetProperty("AvailableSeats")),
                    NumEnrolled = (ushort) JsonFieldToInt(elem.GetProperty("EnrollmentTotal")),
                    // "001"
                    // 100
                    SectionNumber = (ushort) JsonFieldToInt(elem.GetProperty("SectionNumber")),
                    Status = ParseStatus(elem.GetProperty("EnrollmentStatus").GetString()!),
                    Time = now,
                    WaitCapacity = (ushort) JsonFieldToInt(elem.GetProperty("WaitCapacity")),
                    WaitTotal = (ushort) JsonFieldToInt(elem.GetProperty("WaitTotal")),
                    TermCode = term,
                    SectionType = elem.GetProperty("SectionType").GetString()!,
                    SnapshotId = (int)Math.Round((SnapshotTime - new DateTime(2000, 1, 1)).TotalSeconds)
                };
                var response = resJson.RootElement.GetProperty("getSOCSectionsResponse");
                try
                {
                    var sections = response.GetProperty("Section");
                    return sections.ValueKind == JsonValueKind.Array ?
                        sections.EnumerateArray().Select(elemToSection) :
                        new CourseSection[] { elemToSection(sections) };
                }
                catch (KeyNotFoundException)
                {
                    Console.Error.WriteLine($"Warning: {subject} {catalogNumber} may not exist. Ignoring it.");
                    return new CourseSection[] {};
                }
            }
            catch (System.Net.Http.HttpRequestException we)
            {
                if (we.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    Console.WriteLine("Received 429 (Calm Down). Backing off for 10 seconds.");
                    NextRequestTime = DateTime.Now.AddSeconds(10);
                    await Task.Delay(10000);
                    // Outer loop causes restart
                }
                else
                {
                    throw we;
                }
            }
        }
    }

    private CourseSection.EnrollmentStatus ParseStatus(string status)
    {
        switch (status)
        {
            case "Closed": return CourseSection.EnrollmentStatus.Closed;
            case "Wait List": return CourseSection.EnrollmentStatus.Waitlist;
            case "Open": return CourseSection.EnrollmentStatus.Open;
            default: throw new ArgumentOutOfRangeException($"Unknown enrollment status: {status}");
        }
    }

    private static int JsonFieldToInt(in JsonElement elem)
    {
        return (elem.ValueKind == JsonValueKind.Number) ?
            elem.GetInt32() : int.Parse(elem.GetString()!);
    }

    public async Task EnsureRefreshedTokenLoaded()
    {
        if (AccessToken == null || DateTime.Now > AccessTokenExpirationTime)
        {
            await RefreshToken();
        }
        lock (headerLock)
        {
            if (client.DefaultRequestHeaders.Authorization != null)
                client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Authorization = new("Bearer", AccessToken!);
        }
    }

    private async Task RefreshToken()
    {
        var res = await client.PostAsync("https://gw.api.it.umich.edu/um/oauth2/token", new FormUrlEncodedContent(new Dictionary<string, string>{
            {"grant_type", "client_credentials"},
            {"client_id", Secrets.ClientId},
            {"client_secret", Secrets.ClientSecret},
            {"scope", "umscheduleofclasses"}
        }));
        res.EnsureSuccessStatusCode();
        var doc = JsonDocument.Parse(res.Content.ReadAsStream());
        AccessToken = doc.RootElement.GetProperty("access_token").GetString();
        AccessTokenExpirationTime = DateTime.Now.AddSeconds(doc.RootElement.GetProperty("expires_in").GetInt32());
    }
}