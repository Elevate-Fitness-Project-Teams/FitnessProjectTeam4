using FluentValidation;

namespace WorkoutService.Features.Common.Queries.GetWorkoutSession
{
    public class GetWorkoutSessionByIdQueryValidator : AbstractValidator<GetWorkoutSessionByIdQuery>
    {
        public GetWorkoutSessionByIdQueryValidator()
        {
            RuleFor(x => x.SessionId)
                  .NotEmpty()
                  .WithMessage("Session ID must not be empty.");
        }
    }
}
