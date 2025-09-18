namespace LoginBridge.Entities;

public class User
{
    public long UserId { get; set; }

    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<LoginProvider> LoginProviders { get; set; }
}
