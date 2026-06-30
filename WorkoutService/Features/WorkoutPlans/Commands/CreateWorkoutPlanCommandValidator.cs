using FluentValidation;

namespace WorkoutService.Features.WorkoutPlans.Commands
{
    public class CreateWorkoutPlanCommandValidator : AbstractValidator<CreateWorkoutPlanCommand>
    {
        public CreateWorkoutPlanCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("REQUIRED_FIELD")
                .MaximumLength(150).WithMessage("INVALID_INPUT");

            RuleFor(x => x.UserTier)
                .NotEmpty().WithMessage("REQUIRED_FIELD")
                .Must(tier => tier == "Free" || tier == "Premium").WithMessage("INVALID_INPUT");
        }
    }
}
