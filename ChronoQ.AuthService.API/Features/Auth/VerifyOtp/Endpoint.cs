using FluentValidation;

namespace ChronoQ.AuthService.API.Features.Auth.VerifyOtp;

public static class VerifyOtpEndpoint
{
    public static void MapVerifyOtp(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/verify-otp", async (
            VerifyOtpCommand command,
            IValidator<VerifyOtpCommand> validator,
            VerifyOtpHandler handler) =>
        {
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            return await handler.HandleAsync(command);
        });
    }
}