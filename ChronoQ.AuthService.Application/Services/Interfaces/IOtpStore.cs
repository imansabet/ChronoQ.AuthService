namespace ChronoQ.AuthService.Application.Services.Interfaces;

public interface IOtpStore
{
    Task SetOtpAsync(string phoneNumber, string code, TimeSpan ttl);
    Task<string?> GetOtpAsync(string phoneNumber);
    Task RemoveOtpAsync(string phoneNumber);
}