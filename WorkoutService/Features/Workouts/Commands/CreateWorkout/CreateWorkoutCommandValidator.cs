using FluentValidation;

namespace WorkoutService.Features.Workouts.Commands.CreateWorkout
{
    public class CreateWorkoutCommandValidator : AbstractValidator<CreateWorkoutCommand>
    {
        public CreateWorkoutCommandValidator()
        {
            RuleFor(x => x.WorkoutPlanId)
                .NotEmpty()
                .WithMessage("Workout Plan Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Workout name is required.")
                .MaximumLength(100)
                .WithMessage("Workout name cannot exceed 100 characters.");

            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("Category is required.")
                .MaximumLength(50)
                .WithMessage("Category cannot exceed 50 characters.");

            RuleFor(x => x.Difficulty)
                .NotEmpty()
                .WithMessage("Difficulty is required.")
                .Must(d => new[] { "Beginner", "Intermediate", "Advanced" }.Contains(d))
                .WithMessage("Difficulty must be Beginner, Intermediate, or Advanced.");

            RuleFor(x => x.DurationInMinutes)
                .GreaterThan(0)
                .WithMessage("Duration must be greater than 0 minutes.")
                .LessThanOrEqualTo(600)
                .WithMessage("Duration cannot exceed 600 minutes.");

            RuleFor(x => x.CaloriesBurn)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Calories burned cannot be negative.")
                .LessThanOrEqualTo(5000)
                .WithMessage("Calories burned cannot exceed 5000.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .WithMessage("Image URL is required.")
                .MaximumLength(500)
                .WithMessage("Image URL cannot exceed 500 characters.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Image URL must be a valid absolute URL.");

            RuleFor(x => x.DayNumber)
                .GreaterThan(0)
                .WithMessage("Day number must be greater than 0.")
                .LessThanOrEqualTo(31)
                .WithMessage("Day number cannot exceed 31.");
        }
    }
}
