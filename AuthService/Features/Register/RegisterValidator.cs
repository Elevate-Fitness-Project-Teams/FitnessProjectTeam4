using AuthService.Common.Api;
using FluentValidation;
using System.Text.RegularExpressions;

namespace AuthService.Features.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    private static readonly Regex PasswordRule =
        new(@"^(?=.*[A-Z])(?=.*\d).{6,}$", RegexOptions.Compiled);

    private static readonly Regex EgyptianPhone =
        new(@"^\+20\d{10}$", RegexOptions.Compiled);

    public RegisterValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("First name is required.")
            .Length(2, 50)
                .WithErrorCode(ErrorCodes.VAL_INVALID_LENGTH)
                .WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("Last name is required.")
            .Length(2, 50)
                .WithErrorCode(ErrorCodes.VAL_INVALID_LENGTH)
                .WithMessage("Last name must be between 2 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("Email is required.")
            .EmailAddress()
                .WithErrorCode(ErrorCodes.VAL_INVALID_FORMAT)
                .WithMessage("Email must be a valid email address.")
            .MaximumLength(320)
                .WithErrorCode(ErrorCodes.VAL_INVALID_LENGTH)
                .WithMessage("Email must not exceed 320 characters.");

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("Password is required.")
            .Must(p => PasswordRule.IsMatch(p ?? string.Empty))
                .WithErrorCode(ErrorCodes.AUTH_WEAK_PASSWORD)
                .WithMessage("Password must be at least 6 characters and contain 1 uppercase letter and 1 number.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
                .WithErrorCode(ErrorCodes.VAL_REQUIRED_FIELD)
                .WithMessage("Phone number is required.")
            .Must(p => EgyptianPhone.IsMatch(p ?? string.Empty))
                .WithErrorCode(ErrorCodes.AUTH_INVALID_PHONE)
                .WithMessage("Phone number must be in Egyptian format (+20XXXXXXXXXX).");
    }
}
