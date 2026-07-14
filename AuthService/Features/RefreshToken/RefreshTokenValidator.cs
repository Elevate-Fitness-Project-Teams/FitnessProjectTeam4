using AuthService.Common.Api;
using FluentValidation;

namespace AuthService.Features.RefreshToken;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("Refresh token is required.");
    }
}
