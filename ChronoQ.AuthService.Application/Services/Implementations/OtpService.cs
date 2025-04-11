
using ChronoQ.AuthService.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace ChronoQ.AuthService.Application.Services.Implementations;

public class OtpService : IOtpService
{
    private readonly IOtpStore _otpStore;
    private readonly ILogger<OtpService> _logger;
    private static readonly TimeSpan _otpTtl = TimeSpan.FromMinutes(2);

    public OtpService(IOtpStore otpStore, ILogger<OtpService> logger)
    {
        _otpStore = otpStore;
        _logger = logger;
    }

    public async Task<string> GenerateAndSendOtpAsync(string phoneNumber)
    {
        var code = new Random().Next(100000, 999999).ToString();
        await _otpStore.SetOtpAsync(phoneNumber, code, _otpTtl);

        _logger.LogInformation("OTP for {PhoneNumber}: {Code}", phoneNumber, code);

        return code;
    }

    public async Task<bool> VerifyOtpAsync(string phoneNumber, string code)
    {
        var storedCode = await _otpStore.GetOtpAsync(phoneNumber);
        if (storedCode is null || storedCode != code)
            return false;

        await _otpStore.RemoveOtpAsync(phoneNumber); 
        return true;
    }
}