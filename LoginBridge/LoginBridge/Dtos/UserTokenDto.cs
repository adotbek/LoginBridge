using LoginBridge.Entities;

namespace LoginBridge.Dtos;

public class UserTokenDto
{
    public long UserId { get; set; }

    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
}
