namespace LoginBridge.Dtos;

public class LoginResponseDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; } = null;
    public string TokenType { get; set; } = null;
    public int Expires { get; set; } = 24;

}
