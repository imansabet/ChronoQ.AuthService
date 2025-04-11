using FluentValidation;

namespace ChronoQ.AuthService.API.Features.Auth.RefreshToken;

public static class RefreshTokenEndpoint
{
    public static void MapRefreshToken(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/refresh-token", async (
            RefreshTokenCommand command,
            IValidator<RefreshTokenCommand> validator,
            RefreshTokenHandler handler) =>
        {
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            return await handler.HandleAsync(command);
        });
    }
}