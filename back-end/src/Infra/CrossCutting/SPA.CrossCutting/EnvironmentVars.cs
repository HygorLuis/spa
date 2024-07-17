namespace SPA.CrossCutting;

public class EnvironmentVars
{
    public static readonly string ConnectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING") ?? "";
    public static readonly string JwtSecret = Environment.GetEnvironmentVariable("JWT-SECRET") ?? "";
    public static readonly string JwtIssuer = Environment.GetEnvironmentVariable("JWT-ISSUER") ?? "";
    public static readonly string JwtAudience = Environment.GetEnvironmentVariable("JWT-AUDIENCE") ?? "";
}