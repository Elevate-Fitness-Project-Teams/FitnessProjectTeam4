using FluentValidation;

namespace ProgressService.Features.Progress.Queries.GetWeightHistory
{
    public class GetWeightHistoryQueryValidator : AbstractValidator<GetWeightHistoryQuery>
    {
        public GetWeightHistoryQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User Id is required.")
                .MaximumLength(450)
                .WithMessage("User Id cannot exceed 450 characters.");
        }
    }
}
