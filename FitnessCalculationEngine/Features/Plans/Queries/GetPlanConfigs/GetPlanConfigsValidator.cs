using FitnessCalculationEngine.Common.Api;
using FluentValidation;

namespace FitnessCalculationEngine.Features.Plans.Queries.GetPlanConfigs;

public class GetPlanConfigsValidator : AbstractValidator<GetPlanConfigsQuery>
{
    public GetPlanConfigsValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(ErrorCodes.VAL_INVALID_PAGE)
            .WithMessage("Page must be >= 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithErrorCode(ErrorCodes.VAL_INVALID_PAGE_SIZE)
            .WithMessage("PageSize must be between 1 and 100.");

        When(x => x.Goal is not null, () =>
            RuleFor(x => x.Goal!.Value)
                .IsInEnum()
                .WithErrorCode(ErrorCodes.VAL_INVALID_GOAL)
                .WithMessage("Invalid goal."));

        When(x => x.Status is not null, () =>
            RuleFor(x => x.Status!.Value)
                .IsInEnum()
                .WithErrorCode(ErrorCodes.VAL_INVALID_STATUS)
                .WithMessage("Invalid status."));
    }
}
