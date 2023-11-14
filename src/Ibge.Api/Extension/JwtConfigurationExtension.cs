using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ibge.Api.Extension;

public static class JwtConfigurationExtension
{
    public static IServiceCollection ConfigureAddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:Key"])),
                };
            });

        return services;
    }
}
