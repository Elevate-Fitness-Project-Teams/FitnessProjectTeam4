using AuthService.Common.Api;
using FluentValidation;

namespace AuthService.Features.ForgotPassword;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("Email is required.")
            .EmailAddress()
                .WithErrorCode(ErrorCodes.VAL_INVALID_FORMAT)
                .WithMessage("Email must be a valid email address.");
    }
}
