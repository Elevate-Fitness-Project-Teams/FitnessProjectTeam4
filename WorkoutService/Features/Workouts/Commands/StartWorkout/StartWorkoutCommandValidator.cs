using FluentValidation;

namespace WorkoutService.Features.Workouts.Commands.StartWorkout
{
    public class StartWorkoutCommandValidator : AbstractValidator<StartWorkoutCommand>
    {
        public StartWorkoutCommandValidator()
        {
            RuleFor(x => x.WorkoutId)
               .NotEmpty()
               .WithMessage("Workout Id is required.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User Id is required.");


        }
    }
}
