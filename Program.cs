using Amazon.Lambda.RuntimeSupport;

CourseCode[] codes = {
    new("STATS", 250),
    new("ECON", 101),
    new("ENGLISH", 125),
    new("ENGLISH", 124),
    new("MATH", 115),
    new("MATH", 116),
    new("MATH", 215),
    new("EECS", 280),
    new("CHEM", 130),
    new("CHEM", 125),
    new("PSYCH", 111),
    new("EECS", 183),
    new("ENGR", 101),
    new("ENGR", 100),
    new("CHEM", 210),
    new("CHEM", 211),
    new("ANTHRCUL", 101),
    new("PHYSICS", 140),
    new("BIOLOGY", 171),
    new("BA", 100),
    new("SOC", 100),
    new("ARTDES", 100),
    new("ARTDES", 105),
    new("ARTDES", 115),
    new("ARTDES", 120)
};

async Task AsyncHandler()
{
    var client = new APIClient();
    var db = new CourseDatabase();
    await Parallel.ForEachAsync(codes, async (code, token) =>
    {
        var seq = await client.ReadSections("2410", code.Catalog, code.CatalogNbr);
        await db.SaveCourses(seq);
        Console.WriteLine($"saved {seq.Count()} sections for {code.Catalog} {code.CatalogNbr}");
    });
}

await LambdaBootstrapBuilder.Create(AsyncHandler).Build().RunAsync();
// await AsyncHandler();

readonly struct CourseCode
{
    public string Catalog { get; init; }
    public short CatalogNbr { get; init; }

    public CourseCode(string cat, short nbr)
    {
        Catalog = cat;
        CatalogNbr = nbr;
    }
}