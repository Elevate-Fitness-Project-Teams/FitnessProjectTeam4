using FluentValidation;

namespace WorkoutService.Features.Workouts.Commands.AddExerciseToWorkout
{
    public class AddExerciseToWorkoutCommandValidator : AbstractValidator<AddExerciseToWorkoutCommand>
    {
        public AddExerciseToWorkoutCommandValidator()
        {
            RuleFor(x => x.WorkoutId)
              .NotEmpty()
              .WithMessage("Workout ID is required.");

            RuleFor(x => x.ExerciseId)
                .NotEmpty()
                .WithMessage("Exercise ID is required.");

            RuleFor(x => x.Sets)
                .GreaterThan(0)
                .WithMessage("Sets must be greater than zero.")
                .LessThanOrEqualTo(20)
                .WithMessage("Sets must not exceed 20.");

            RuleFor(x => x.Reps)
               .NotEmpty()
              .WithMessage("Workout ID is required.");
        }
    }
}
