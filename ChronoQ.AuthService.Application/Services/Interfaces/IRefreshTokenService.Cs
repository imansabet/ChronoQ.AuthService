using ChronoQ.AuthService.Domain.Entities;

namespace ChronoQ.AuthService.Application.Services.Interfaces;

public interface IRefreshTokenService
{
    Task<RefreshToken> CreateAsync(Guid userId);
    Task<RefreshToken?> GetAsync(string token);
    Task InvalidateAsync(string token);
}