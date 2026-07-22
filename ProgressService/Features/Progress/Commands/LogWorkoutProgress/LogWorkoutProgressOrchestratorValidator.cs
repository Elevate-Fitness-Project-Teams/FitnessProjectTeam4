using FluentValidation;

namespace ProgressService.Features.Progress.Commands.LogWorkoutProgress
{
    public class LogWorkoutProgressOrchestratorValidator : AbstractValidator<LogWorkoutProgressOrchestrator>
    {
        public LogWorkoutProgressOrchestratorValidator()
        {
            RuleFor(x => x.WorkoutId)
                .NotEmpty();

            RuleFor(x => x.SessionId)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.CompletedAt)
                .NotEmpty();

            RuleFor(x => x.DurationInMinutes)
                .GreaterThan(0)
                .LessThanOrEqualTo(1440);

            RuleFor(x => x.CaloriesBurned)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(10000);

            RuleFor(x => x.Difficulty)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Notes)
                .MaximumLength(1000);

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5);

            RuleFor(x => x.ExercisesCompleted)
                .NotNull()
                .NotEmpty();

            RuleForEach(x => x.ExercisesCompleted)
                .SetValidator(new ExerciseCompletedDtoValidator());
        }

    }

    public class ExerciseCompletedDtoValidator  : AbstractValidator<ExerciseCompletedDto>
    {
        public ExerciseCompletedDtoValidator()
        {
            RuleFor(x => x.ExerciseId)
                .NotEmpty();

            RuleFor(x => x.SetsCompleted)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.RepsCompleted)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.WeightUsed)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x)
                .Must(x =>
                    !x.Completed ||
                    (x.SetsCompleted > 0 && x.RepsCompleted > 0))
                .WithMessage("Completed exercises must have completed sets and reps.");
        }
    }
}
