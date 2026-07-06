using FluentValidation;

namespace WorkoutService.Features.Exercises.Queries.GetExerciseById
{
    public class GetExerciseByIdQueryValidator : AbstractValidator<GetExerciseByIdQuery>
    {
        public GetExerciseByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Exercise ID must not be empty.");
        }
    }
}
