using StackExchange.Redis;
using ChronoQ.AuthService.Application.Services.Interfaces;

namespace ChronoQ.AuthService.Infrastructure.Redis;

public class RedisOtpStore : IOtpStore
{
    private readonly IDatabase _db;

    public RedisOtpStore(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task SetOtpAsync(string phoneNumber, string code, TimeSpan ttl)
    {
        var key = $"otp:{phoneNumber}";
        await _db.StringSetAsync(key, code, ttl);
    }

    public async Task<string?> GetOtpAsync(string phoneNumber)
    {
        var key = $"otp:{phoneNumber}";
        var result = await _db.StringGetAsync(key);
        return result.HasValue ? result.ToString() : null;
    }

    public async Task RemoveOtpAsync(string phoneNumber)
    {
        var key = $"otp:{phoneNumber}";
        await _db.KeyDeleteAsync(key);
    }
}