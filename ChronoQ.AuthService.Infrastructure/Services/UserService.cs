using ChronoQ.AuthService.Application.Services.Interfaces;
using ChronoQ.AuthService.Domain.Entities;
using ChronoQ.AuthService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChronoQ.AuthService.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly AuthDbContext _db;

    public UserService(AuthDbContext db)
    {
        _db = db;
    }

    public async Task<User> GetOrCreateUserAsync(string phoneNumber)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

        if (user is not null)
            return user;

        var newUser = new User
        {
            PhoneNumber = phoneNumber
        };

        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();

        return newUser;
    }
}