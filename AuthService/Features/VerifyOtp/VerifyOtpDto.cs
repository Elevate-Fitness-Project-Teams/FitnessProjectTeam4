namespace AuthService.Features.VerifyOtp;

public record VerifyOtpDto(string Email, string Otp);

public record VerifyOtpResultDto(string ResetToken, DateTime ExpiresAt);
