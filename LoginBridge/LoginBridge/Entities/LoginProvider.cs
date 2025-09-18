namespace LoginBridge.Entities;

public class LoginProvider
{
    public long LoginProviderId { get; set; }
    public string ProviderName { get; set; }
    public string ProviderUserId { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}
