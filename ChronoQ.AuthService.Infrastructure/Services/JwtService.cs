using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChronoQ.AuthService.Application.Models;
using ChronoQ.AuthService.Application.Services.Interfaces;
using ChronoQ.AuthService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ChronoQ.AuthService.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public TokenResult GenerateTokens(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("phone_number", user.PhoneNumber),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new TokenResult(
            AccessToken: new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken: Guid.NewGuid().ToString(), 
            ExpiresIn: 3600
        );
    }
}