using FluentValidation;

namespace ChronoQ.AuthService.API.Features.Auth.RequestOtp;

public static class RequestOtpEndpoint
{
    public static void MapRequestOtp(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/request-otp", async (
            RequestOtpCommand request,
            IValidator<RequestOtpCommand> validator,
            RequestOtpHandler handler) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            return await handler.HandleAsync(request);
        });
    }
}