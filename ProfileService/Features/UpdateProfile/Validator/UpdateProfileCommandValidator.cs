using FluentValidation;
using ProfileService.Features.UpdateProfile.Commands;

namespace ProfileService.Features.UpdateProfile.Validator
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.FirstName) ||
                           !string.IsNullOrWhiteSpace(x.LastName) ||
                           !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage("VAL_AT_LEAST_ONE_FIELD_REQUIRED")
                .WithName("UpdateFields");
        }
    }
}
