
using ChronoQ.AuthService.Application.Services.Interfaces;

namespace ChronoQ.AuthService.API.Features.Auth.RequestOtp;

public class RequestOtpHandler
{
    private readonly IOtpService _otpService;

    public RequestOtpHandler(IOtpService otpService)
    {
        _otpService = otpService;
    }

    public async Task<IResult> HandleAsync(RequestOtpCommand command)
    {
        await _otpService.GenerateAndSendOtpAsync(command.PhoneNumber);
        return Results.Ok(new { message = "OTP sent (logged in console for demo)." });
    }
}