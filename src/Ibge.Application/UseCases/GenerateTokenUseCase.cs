using Ibge.Application.Configuration;
using Ibge.Domain.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ibge.Application.UseCases;

public class GenerateTokenUseCase
{
    private readonly JwtOptions _options;

    public GenerateTokenUseCase(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public virtual string Action(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        ArgumentNullException.ThrowIfNull(_options.Key);

        var key = Encoding.ASCII.GetBytes(_options.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            }),

            Expires = DateTime.UtcNow.AddMinutes(_options.TimeToExpiresInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
