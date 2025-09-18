using LoginBridge.Api.Configurations.Settings;
using LoginBridge.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginBridge.Services;


public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string GenerateToken(UserTokenDto tokenDto)
    {
        var IdentityClaims = new Claim[]
        {
            new Claim("UserId",tokenDto.UserId.ToString()),
            new Claim("FirstName",tokenDto.FirstName.ToString()),
            new Claim("LastName",tokenDto.LastName.ToString()),
            new Claim("UserName",tokenDto.UserName.ToString()),
            new Claim(ClaimTypes.Role,tokenDto.Role.ToString()),
            new Claim(ClaimTypes.Email,tokenDto.Email.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresHours = int.Parse(_jwtSettings.Lifetime);
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: IdentityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
