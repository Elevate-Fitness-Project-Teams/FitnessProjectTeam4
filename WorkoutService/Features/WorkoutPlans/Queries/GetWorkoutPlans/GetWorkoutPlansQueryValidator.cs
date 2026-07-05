using FluentValidation;

namespace WorkoutService.Features.WorkoutPlans.Queries.GetWorkoutPlans
{
    public class GetWorkoutPlansQueryValidator : AbstractValidator<GetWorkoutPlansQuery>
    {
        public GetWorkoutPlansQueryValidator()
        {
            RuleFor(x => x.PageNumber)
          .GreaterThanOrEqualTo(1)
          .WithMessage("Page number must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50)
                .WithMessage("Page size must be between 1 and 50.");
        }
    }
}
