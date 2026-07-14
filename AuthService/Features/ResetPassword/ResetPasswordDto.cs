namespace AuthService.Features.ResetPassword;

public record ResetPasswordDto(string ResetToken, string NewPassword, string ConfirmPassword);

public record ResetPasswordResultDto(bool Reset);
