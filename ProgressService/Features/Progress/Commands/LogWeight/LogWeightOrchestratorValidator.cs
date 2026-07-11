using FluentValidation;

namespace ProgressService.Features.Progress.Commands.LogWeight
{
    public class LogWeightOrchestratorValidator : AbstractValidator<LogWeightCommand>
    {
        public LogWeightOrchestratorValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User Id is required.")
                .MaximumLength(450)
                .WithMessage("User Id cannot exceed 450 characters.");

            RuleFor(x => x.Weight)
                .InclusiveBetween(40, 200)
                .WithMessage("Weight must be between 40 kg and 200 kg.");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Weight date cannot be in the future.");

            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .WithMessage("Notes cannot exceed 500 characters.");
        }
    }
}
