class Secrets
{
    // format: UUID
    public readonly static string ClientId = Environment.GetEnvironmentVariable("UM_API_CLIENT_ID")!;
    // format: base64
    public readonly static string ClientSecret = Environment.GetEnvironmentVariable("UM_API_CLIENT_SECRET")!;
}