namespace AuthService.Common.Security;

public record IssuedToken(string Token, DateTime ExpiresAt);

public interface IJwtTokenService
{
    IssuedToken CreateAccessToken(Guid userId, string email);
    (string RawToken, string TokenHash, DateTime ExpiresAt) CreateRefreshToken();
    string HashRefreshToken(string rawToken);
    IssuedToken CreateResetToken(string email);
    string? ValidateResetToken(string token);
}
