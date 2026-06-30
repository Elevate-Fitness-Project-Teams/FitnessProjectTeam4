using FluentValidation;

namespace WorkoutService.Features.Workouts.Queries.GetWorkoutById
{
    public class GetWorkoutByIdQueryValidator : AbstractValidator<GetWorkoutByIdQuery>
    {
        public GetWorkoutByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Workout ID must not be empty.");
        }
    }
}
