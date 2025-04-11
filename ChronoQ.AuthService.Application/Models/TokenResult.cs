namespace ChronoQ.AuthService.Application.Models;

public record TokenResult(string AccessToken, string RefreshToken, int ExpiresIn);