using FluentValidation;

namespace ChronoQ.AuthService.API.Features.Auth.RequestOtp;

public class RequestOtpValidator : AbstractValidator<RequestOtpCommand>
{
    public RequestOtpValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^09\d{9}$").WithMessage("Invalid phone number format.");
    }
}