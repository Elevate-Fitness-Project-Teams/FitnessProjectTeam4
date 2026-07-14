namespace AuthService.Features.ForgotPassword;

public record ForgotPasswordDto(string Email);

public record ForgotPasswordResultDto(bool Sent, int OtpExpiresIn, int CanResendIn);
