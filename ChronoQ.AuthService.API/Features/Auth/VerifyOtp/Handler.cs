using ChronoQ.AuthService.Application.Services.Interfaces;

namespace ChronoQ.AuthService.API.Features.Auth.VerifyOtp;

public class VerifyOtpHandler
{
    private readonly IOtpService _otpService;
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public VerifyOtpHandler(IOtpService otpService, IUserService userService, IJwtService jwtService)
    {
        _otpService = otpService;
        _userService = userService;
        _jwtService = jwtService;
    }

    public async Task<IResult> HandleAsync(VerifyOtpCommand command)
    {
        var isValid = await _otpService.VerifyOtpAsync(command.PhoneNumber, command.Code);
        if (!isValid)
            return Results.BadRequest(new { message = "Invalid or expired OTP." });

        var user = await _userService.GetOrCreateUserAsync(command.PhoneNumber);
        var tokens = await _jwtService.GenerateTokensAsync(user);

        return Results.Ok(tokens);
    }
}