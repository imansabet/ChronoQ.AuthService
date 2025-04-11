namespace ChronoQ.AuthService.API.Features.Auth.VerifyOtp;

public record VerifyOtpCommand(string PhoneNumber, string Code);