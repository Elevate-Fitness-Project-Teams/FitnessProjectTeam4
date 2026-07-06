using FluentValidation;

namespace WorkoutService.Features.Workouts.Queries.GetWorkouts
{
    public class GetWorkoutsQueryValidator : AbstractValidator<GetWorkoutsQuery>
    {
        public GetWorkoutsQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50)
                .WithMessage("Page size must be between 1 and 50.");
        }
    }
}
