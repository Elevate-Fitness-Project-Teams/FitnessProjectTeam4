using FluentValidation;

namespace ProgressService.Features.Progress.Queries.GetUserStats
{
    public class GetUserStatsQueryValidator : AbstractValidator<GetUserStatsQuery>
    {
        public GetUserStatsQueryValidator()
        {
            RuleFor(x => x.UserId)
               .NotEmpty()
               .WithMessage("User Id is required.")
               .MaximumLength(450)
               .WithMessage("User Id cannot exceed 450 characters.");
        }
    }
}
