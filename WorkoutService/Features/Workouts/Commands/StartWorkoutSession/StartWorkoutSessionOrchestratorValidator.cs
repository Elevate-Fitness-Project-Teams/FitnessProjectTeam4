using FluentValidation;

namespace WorkoutService.Features.Workouts.Commands.StartWorkout
{
    public class StartWorkoutSessionOrchestratorValidator : AbstractValidator<StartWorkoutSessionOrchestrator>
    {
        public StartWorkoutSessionOrchestratorValidator()
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
