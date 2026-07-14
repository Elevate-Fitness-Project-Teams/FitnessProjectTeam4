using AuthService.Common.Api;
using FluentValidation;

namespace AuthService.Features.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("Email is required.")
            .EmailAddress()
                .WithErrorCode(ErrorCodes.VAL_INVALID_FORMAT)
                .WithMessage("Email must be a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("Password is required.");
    }
}
