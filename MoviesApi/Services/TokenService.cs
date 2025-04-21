using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MoviesApi.Config;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly string? _key = Configuration.GetSectionValue("Jwt", "Key");
    private readonly string? _expireMinutes = Configuration.GetSectionValue("Jwt", "ExpireMinutes");
    public string GenerateToken(string username)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key ?? string.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_expireMinutes ?? "0")),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}