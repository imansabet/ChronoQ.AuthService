using ChronoQ.AuthService.Domain.Entities;

namespace ChronoQ.AuthService.Application.Services.Interfaces;

public interface IUserService
{
    Task<User> GetOrCreateUserAsync(string phoneNumber);
}