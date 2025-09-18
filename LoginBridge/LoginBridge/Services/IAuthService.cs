using LoginBridge.Dtos;

namespace LoginBridge.Services;

public interface IAuthService
{
    Task<LoginResponseDto> GoogleLoginAsync(GoogleAuthDto dto);
    Task<long> GoogleRegisterAsync(GoogleAuthDto dto);
    Task<long> SignUpUserAsync(RegisterDto userCreateDto);
    Task<LoginResponseDto> LoginUserAsync(LoginDto userLoginDto);
}
