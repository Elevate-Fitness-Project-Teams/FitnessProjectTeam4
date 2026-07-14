using FluentValidation;

namespace ProgressService.Features.Progress.Common.Queries.GetUserStatistics
{
    public class GetUserStatisticsQueryValidator : AbstractValidator<GetUserStatisticsQuery>
    {
        public GetUserStatisticsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .MaximumLength(50).WithMessage("UserId cannot exceed 50 characters.");
        }
    }
}
