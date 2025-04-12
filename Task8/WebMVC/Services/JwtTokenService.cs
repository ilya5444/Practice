using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebMVC.Constants;

namespace WebMVC.Services;

public class JwtTokenService : ITokenService
{
    private readonly IConfiguration configuration;

    public JwtTokenService(IConfiguration configuration)
        => this.configuration = configuration;

    public string GenerateToken(string userId, string role)
    {
        var bytes = Encoding.UTF8.GetBytes(configuration[Jwt.KeyPath]!);
        SymmetricSecurityKey key = new(bytes);

        JwtSecurityToken token = new(
                issuer: configuration[Jwt.IssuerPath],
                audience: configuration[Jwt.AudiencePath],
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                claims: [new(Jwt.UserIdClaimType, userId), new(Jwt.RoleClaimType, role)]
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}