using ChronoQ.AuthService.Application.Models;
using ChronoQ.AuthService.Domain.Entities;

namespace ChronoQ.AuthService.Application.Services.Interfaces;

public interface IJwtService
{
    Task<TokenResult> GenerateTokensAsync(User user);
}