using LoginBridge.Dtos;
using LoginBridge.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginBridge.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService AuthService;
    public AuthController(IAuthService authService)
    {
        AuthService = authService;
    }

    [HttpPost("register")]
    public async Task<long> Register(RegisterDto userCreateDto)
    {
        return await AuthService.SignUpUserAsync(userCreateDto);
    }

    [HttpPost("login")]
    public async Task<LoginResponseDto> Login(LoginDto userLoginDto)
    {
        return await AuthService.LoginUserAsync(userLoginDto);
    }

    [HttpPost("google/register")]
    public async Task<long> GoogleRegister(GoogleAuthDto googleAuthDto)
    {
        return await AuthService.GoogleRegisterAsync(googleAuthDto);
    }

    [HttpPost("google/login")]
    public async Task<LoginResponseDto> GoogleLogin(GoogleAuthDto googleAuthDto)
    {
        return await AuthService.GoogleLoginAsync(googleAuthDto);
    }
}
