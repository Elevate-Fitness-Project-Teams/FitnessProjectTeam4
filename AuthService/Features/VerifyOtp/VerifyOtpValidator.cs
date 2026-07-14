using AuthService.Common.Api;
using FluentValidation;

namespace AuthService.Features.VerifyOtp;

public class VerifyOtpValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("Email is required.")
            .EmailAddress()
                .WithErrorCode(ErrorCodes.VAL_INVALID_FORMAT)
                .WithMessage("Email must be a valid email address.");

        RuleFor(x => x.Otp)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("OTP code is required.")
            .Matches(@"^\d{6}$")
                .WithErrorCode(ErrorCodes.VAL_INVALID_FORMAT)
                .WithMessage("OTP code must be exactly 6 digits.");
    }
}
