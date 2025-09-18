using Google.Apis.Auth;
using LoginBridge.Api.Persistence;
using LoginBridge.Dtos;
using LoginBridge.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoginBridge.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthService(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDto> GoogleLoginAsync(GoogleAuthDto dto)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken, new GoogleJsonWebSignature.ValidationSettings());

        var provider = await _context.LoginProviders.Include(x => x.User).FirstOrDefaultAsync(u => u.ProviderUserId == payload.Subject);

        if (provider == null)
        {
            throw new Exception("You are not registered yet");
        }

        var userTokenDto = new UserTokenDto
        {
            UserId = provider.User.UserId,
            UserName = provider.User.UserName,
            FirstName = provider.User.FirstName,
            LastName = provider.User.LastName,
            Email = provider.User.Email,
            Role = provider.User.Role
        };

        var token = _tokenService.GenerateToken(userTokenDto);

        var loginResponseDto = new LoginResponseDto
        {
            AccessToken = token,
        };

        return loginResponseDto;
    }

    public async Task<long> GoogleRegisterAsync(GoogleAuthDto dto)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken, new GoogleJsonWebSignature.ValidationSettings());

        var exsistsUser = await _context.LoginProviders.FirstOrDefaultAsync(u => u.ProviderUserId == payload.Subject);

        if (exsistsUser != null)
        {
            return exsistsUser.UserId;
        }

        var user = new User
        {
            UserName = payload.Email.Split('@')[0],
            FirstName = payload.GivenName,
            LastName = payload.FamilyName,
            Email = payload.Email,
            EmailConfirmed = payload.EmailVerified,
            Role = UserRole.User,
            CreatedAt = DateTime.UtcNow
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var provider = new LoginProvider
        {
            UserId = user.UserId,
            ProviderUserId = payload.Subject,
            ProviderName = "Google",
        };

        await _context.LoginProviders.AddAsync(provider);
        await _context.SaveChangesAsync();

        return user.UserId;
    }

    public async Task<LoginResponseDto> LoginUserAsync(LoginDto userLoginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == userLoginDto.EmailOrUserName
                || u.UserName == userLoginDto.EmailOrUserName);

        if (user == null)
        {
            throw new Exception("UserName or password incorrect");
        }

        var checkUserPassword = PasswordHasher.Verify(userLoginDto.Password, user.PasswordHash, user.Salt);

        if (checkUserPassword == false)
        {
            throw new Exception("UserName or password incorrect");
        }

        var userTokenDto = new UserTokenDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role
        };

        var token = _tokenService.GenerateToken(userTokenDto);

        var loginResponseDto = new LoginResponseDto()
        {
            AccessToken = token,
        };

        return loginResponseDto;
    }

    public async Task<long> SignUpUserAsync(RegisterDto userCreateDto)
    {
        var tupleFromHasher = PasswordHasher.Hasher(userCreateDto.Password);
        var user = new User()
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            UserName = userCreateDto.UserName,
            Email = userCreateDto.Email,
            PasswordHash = tupleFromHasher.Hash,
            Salt = tupleFromHasher.Salt,
            Role = UserRole.User,
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.UserId;
    }
}