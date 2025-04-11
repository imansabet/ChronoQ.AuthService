using ChronoQ.AuthService.Application.Services.Interfaces;

namespace ChronoQ.AuthService.API.Features.Auth.RefreshToken;

public class RefreshTokenHandler
{
    private readonly IRefreshTokenService _refreshService;
    private readonly IJwtService _jwtService;

    public RefreshTokenHandler(IRefreshTokenService refreshService, IJwtService jwtService)
    {
        _refreshService = refreshService;
        _jwtService = jwtService;
    }

    public async Task<IResult> HandleAsync(RefreshTokenCommand command)
    {
        var tokenEntity = await _refreshService.GetAsync(command.RefreshToken);

        if (tokenEntity is null || tokenEntity.User is null)
            return Results.BadRequest(new { message = "Invalid refresh token." });

        // Rotation 
        await _refreshService.InvalidateAsync(command.RefreshToken);

        var newTokens = await _jwtService.GenerateTokensAsync(tokenEntity.User);
        return Results.Ok(newTokens);
    }
}