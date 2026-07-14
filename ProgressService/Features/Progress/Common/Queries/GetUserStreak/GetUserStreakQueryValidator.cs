using FluentValidation;

namespace ProgressService.Features.Progress.Common.Queries.GetStreakByUserId
{
    public class GetUserStreakQueryValidator : AbstractValidator<GetUserStreakQuery>
    {
        public GetUserStreakQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
            .MaximumLength(50).WithMessage("UserId cannot exceed 50 characters.");
        }
    }
}
