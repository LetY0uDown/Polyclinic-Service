using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Host.Services;

/// <summary>
/// Класс для генерации JWT токенов, необходимых для авторизации
/// </summary>
public sealed class JWTTokenGenerator
{
    private readonly IConfiguration _config;

    private readonly SymmetricSecurityKey _securityKey;

    public JWTTokenGenerator (IConfiguration config)
    {
        _config = config;

        _securityKey = new(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
    }

    public string GetToken (string email, string password)
    {
        List<Claim> claims = new() {
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Hash, password)
        };

        var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new(_securityKey, SecurityAlgorithms.HmacSha256)
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}