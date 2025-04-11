namespace ChronoQ.AuthService.Application.Services.Interfaces;

public interface IOtpService
{
    Task<string> GenerateAndSendOtpAsync(string phoneNumber);
    Task<bool> VerifyOtpAsync(string phoneNumber, string code);
}