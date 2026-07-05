using FluentValidation;

namespace WorkoutService.Features.Progress.Commands.LogWorkoutProgress
{
    public class LogWorkoutProgressCommandValidator : AbstractValidator<LogWorkoutProgressCommand>
    {
        public LogWorkoutProgressCommandValidator()
        {
            RuleFor(x => x.WorkoutId)
            .NotEmpty()
            .WithMessage("Workout Id is required.");

            RuleFor(x => x.SessionId)
                .NotEmpty()
                .WithMessage("Session Id is required.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User Id is required.");

            RuleFor(x => x.CompletedAt)
                .NotEmpty()
                .WithMessage("Completed time is required.");

            RuleFor(x => x.Duration)
                .InclusiveBetween(1, 1440)
                .WithMessage("Duration must be between 1 and 1440 minutes.");

            RuleFor(x => x.CaloriesBurned)
                .InclusiveBetween(0, 10000)
                .WithMessage("Calories burned must be between 0 and 10000.");

            RuleFor(x => x.Difficulty)
                .NotEmpty()
                .WithMessage("Difficulty is required.");

            RuleFor(x => x.Notes)
                .MaximumLength(1000)
                .WithMessage("Notes cannot exceed 1000 characters.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.ExercisesCompleted)
                .NotNull()
                .WithMessage("Exercises completed are required.")
                .NotEmpty()
                .WithMessage("At least one completed exercise is required.");
        }
    }
}
