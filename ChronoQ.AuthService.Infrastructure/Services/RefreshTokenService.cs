
using ChronoQ.AuthService.Application.Services.Interfaces;
using ChronoQ.AuthService.Domain.Entities;
using ChronoQ.AuthService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChronoQ.AuthService.Infrastructure.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly AuthDbContext _db;

    public RefreshTokenService(AuthDbContext db)
    {
        _db = db;
    }

    public async Task<RefreshToken> CreateAsync(Guid userId)
    {
        var token = Guid.NewGuid().ToString();
        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(7) 
        };

        _db.RefreshTokens.Add(refreshToken);
        await _db.SaveChangesAsync();

        return refreshToken;
    }

    public async Task<RefreshToken?> GetAsync(string token)
    {
        return await _db.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token && rt.ExpiresAt > DateTime.UtcNow);
    }

    public async Task InvalidateAsync(string token)
    {
        var rt = await _db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
        if (rt is not null)
        {
            _db.RefreshTokens.Remove(rt);
            await _db.SaveChangesAsync();
        }
    }
}