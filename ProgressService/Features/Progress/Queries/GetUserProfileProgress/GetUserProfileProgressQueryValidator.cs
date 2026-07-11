using FluentValidation;

namespace ProgressService.Features.Progress.Queries.GetUserProfileProgress
{
    public class GetUserProfileProgressQueryValidator : AbstractValidator<GetUserProfileProgressQuery>
    {
        public GetUserProfileProgressQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User Id is required.")
                .MaximumLength(450)
                .WithMessage("User Id cannot exceed 450 characters.");
        }
    }
}
