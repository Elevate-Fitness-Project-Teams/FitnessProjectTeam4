using FluentValidation;

namespace WorkoutService.Features.Workouts.Commands.CompleteWorkoutSession
{
    public class CompleteWorkoutSessionCommandValidator : AbstractValidator<CompleteWorkoutSessionCommand>
    {
        public CompleteWorkoutSessionCommandValidator()
        {
            RuleFor(x => x.SessionId)
                .NotEmpty()
                .WithMessage("Session Id is required.");
        }
    }
}
