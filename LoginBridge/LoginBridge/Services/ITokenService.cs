using LoginBridge.Dtos;

namespace LoginBridge.Services;

public interface ITokenService
{
    public string GenerateToken(UserTokenDto tokenDto);
}
