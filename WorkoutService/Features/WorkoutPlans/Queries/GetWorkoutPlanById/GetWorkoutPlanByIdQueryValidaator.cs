using FluentValidation;

namespace WorkoutService.Features.WorkoutPlans.Queries.GetWorkoutPlanById
{
    public class GetWorkoutPlanByIdQueryValidaator : AbstractValidator<GetWorkoutPlanByIdQuery>
    {
        public GetWorkoutPlanByIdQueryValidaator()
        {
            RuleFor(x => x.Id)
                  .NotEmpty()
                  .WithMessage("Workout Plan ID must not be empty.");
        }
    }
}
