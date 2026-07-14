namespace AuthService.Features.Login;

public record LoginDto(string Email, string Password);

public record LoginResultDto(
    string Token,
    string RefreshToken,
    bool ProfileCompleted,
    bool IsPremium);
