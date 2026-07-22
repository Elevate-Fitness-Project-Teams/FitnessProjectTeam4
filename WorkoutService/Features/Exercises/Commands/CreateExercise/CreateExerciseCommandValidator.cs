using FluentValidation;

namespace WorkoutService.Features.Exercises.Commands.CreateExercise
{
    public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Exercise name is required.")
                .MaximumLength(150)
                .WithMessage("Exercise name must not exceed 150 characters.");

            RuleFor(x => x.TargetMuscles)
                .NotNull()
                .WithMessage("Target muscles are required.")
                .Must(muscles => muscles != null && muscles.Any())
                .WithMessage("At least one target muscle must be provided.");

            RuleForEach(x => x.TargetMuscles)
                .NotEmpty()
                .WithMessage("Target muscle cannot be empty.")
                .MaximumLength(100)
                .WithMessage("Target muscle must not exceed 100 characters.");

            RuleFor(x => x.EquipmentNeeded)
                .NotEmpty()
                .WithMessage("Equipment needed is required.")
                .MaximumLength(200)
                .WithMessage("Equipment needed must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Exercise description is required.")
                .MaximumLength(1000)
                .WithMessage("Exercise description must not exceed 1000 characters.");

            RuleFor(x => x.Difficulty)
                .NotEmpty()
                .WithMessage("Exercise difficulty is required.")
                .MaximumLength(50)
                .WithMessage("Exercise difficulty must not exceed 50 characters.");

            RuleFor(x => x.VideoUrl)
                .NotEmpty()
                .WithMessage("Exercise video URL is required.")
                .MaximumLength(500)
                .WithMessage("Exercise video URL must not exceed 500 characters.");
        }
    }
}
