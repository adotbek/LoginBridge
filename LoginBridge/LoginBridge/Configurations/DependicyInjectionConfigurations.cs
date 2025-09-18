using LoginBridge.Services;

namespace LoginBridge.Configurations;

public static class DependicyInjectionConfigurations
{
    public static void ConfigureDI(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
    }
}
