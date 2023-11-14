namespace Ibge.Application.Configuration;

public class JwtOptions
{
    public string Key { get; set; } = string.Empty;
    public int TimeToExpiresInMinutes { get; set; }
}
