using FluentValidation;

namespace ChronoQ.AuthService.API.Features.Auth.VerifyOtp;

public class VerifyOtpValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().Matches(@"^09\d{9}$");
        
        RuleFor(x => x.Code)
            .NotEmpty().Length(6);
    }
}