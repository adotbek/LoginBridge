namespace LoginBridge.Api.Configurations.Settings;

public class JwtSettings
{
    public string SecurityKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Lifetime { get; set; }
}
