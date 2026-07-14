using FitnessCalculationEngine.Common.Api;
using FluentValidation;

namespace FitnessCalculationEngine.Features.Recalculation.Commands.Recalculate;

public class RecalculateValidator : AbstractValidator<RecalculateCommand>
{
    public RecalculateValidator()
    {
        When(x => x.NewWeight is not null, () =>
        {
            RuleFor(x => x.NewWeight!.Value)
                .InclusiveBetween(40m, 200m)
                .WithErrorCode(ErrorCodes.VAL_INVALID_WEIGHT)
                .WithMessage("Weight must be between 40 and 200 kg.");
        });
    }
}
