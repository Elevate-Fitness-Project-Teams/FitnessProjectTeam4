namespace AuthService.Features.RefreshToken;

public record RefreshTokenDto(string RefreshToken);

public record RefreshTokenResultDto(string Token, string RefreshToken);
