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
    private readonly IRefreshTokenService _refreshTokenService;

    public JwtService(IConfiguration config, IRefreshTokenService refreshTokenService)
    {
        _config = config;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<TokenResult> GenerateTokensAsync(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("phone_number", user.PhoneNumber),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var accessToken = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        var refreshToken = await _refreshTokenService.CreateAsync(user.Id);

        return new TokenResult(
            AccessToken: new JwtSecurityTokenHandler().WriteToken(accessToken),
            RefreshToken: refreshToken.Token,
            ExpiresIn: 3600
        );
    }
}