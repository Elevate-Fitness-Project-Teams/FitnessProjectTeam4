using AuthService.Common.Api;
using FluentValidation;
using System.Text.RegularExpressions;

namespace AuthService.Features.ResetPassword;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    private static readonly Regex PasswordRule =
        new(@"^(?=.*[A-Z])(?=.*\d).{6,}$", RegexOptions.Compiled);

    public ResetPasswordValidator()
    {
        RuleFor(x => x.ResetToken)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("Reset token is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("New password is required.")
            .Must(p => PasswordRule.IsMatch(p ?? string.Empty))
                .WithErrorCode(ErrorCodes.AUTH_WEAK_PASSWORD)
                .WithMessage("Password must be at least 6 characters and contain 1 uppercase letter and 1 number.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("Confirm password is required.");
    }
}
