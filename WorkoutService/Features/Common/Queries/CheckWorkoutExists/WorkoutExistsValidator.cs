using FluentValidation;

namespace WorkoutService.Features.Common.Queries.CheckWorkoutExists
{
    public class WorkoutExistsValidator : AbstractValidator<WorkoutExistsQuery>
    {
        public WorkoutExistsValidator()
        {
            RuleFor(x => x.WorkoutId)
                .NotEmpty()
                .WithMessage("Workout ID must not be empty.");
        }
    }
}
