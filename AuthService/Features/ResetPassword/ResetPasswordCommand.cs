using MediatR;

namespace AuthService.Features.ResetPassword;

public record ResetPasswordCommand(string ResetToken, string NewPassword, string ConfirmPassword)
    : IRequest<ResetPasswordResultDto>;
