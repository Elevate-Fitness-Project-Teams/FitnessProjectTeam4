using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Domain.Enums;
using FluentValidation;

namespace FitnessCalculationEngine.Features.FitnessStats.Commands.SubmitStats;

public class SubmitStatsValidator : AbstractValidator<SubmitStatsCommand>
{
    public SubmitStatsValidator()
    {
        RuleFor(x => x.Age)
            .InclusiveBetween(16, 100)
            .WithErrorCode(ErrorCodes.VAL_INVALID_AGE)
            .WithMessage("Age must be between 16 and 100.");

        RuleFor(x => x.Weight)
            .InclusiveBetween(40m, 200m)
            .WithErrorCode(ErrorCodes.VAL_INVALID_WEIGHT)
            .WithMessage("Weight must be between 40 and 200 kg.");

        RuleFor(x => x.Height)
            .InclusiveBetween(140m, 220m)
            .WithErrorCode(ErrorCodes.VAL_INVALID_HEIGHT)
            .WithMessage("Height must be between 140 and 220 cm.");

        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithErrorCode(ErrorCodes.VAL_INVALID_GENDER)
            .WithMessage("Gender must be Male or Female.");

        RuleFor(x => x.Goal)
            .IsInEnum()
            .WithErrorCode(ErrorCodes.VAL_INVALID_GOAL)
            .WithMessage($"Goal must be one of: {string.Join(", ", Enum.GetNames<Goal>())}.");

        RuleFor(x => x.ActivityLevel)
            .IsInEnum()
            .WithErrorCode(ErrorCodes.VAL_INVALID_ACTIVITY)
            .WithMessage($"ActivityLevel must be one of: {string.Join(", ", Enum.GetNames<ActivityLevel>())}.");
    }
}
