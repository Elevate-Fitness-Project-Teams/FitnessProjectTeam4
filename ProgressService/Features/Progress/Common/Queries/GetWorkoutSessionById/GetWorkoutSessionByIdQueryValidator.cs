using FluentValidation;

namespace ProgressService.Features.Progress.Common.Queries.GetWorkoutSessionById
{
    public class GetWorkoutSessionByIdQueryValidator : AbstractValidator<GetWorkoutSessionByIdQuery>    
    {
        public GetWorkoutSessionByIdQueryValidator()
        {
            RuleFor(x => x.sessionId)
                .NotEmpty().WithMessage("Session ID must not be empty.")
                .Must(id => id != Guid.Empty).WithMessage("Session ID must be a valid GUID.");
        }
    }
}
