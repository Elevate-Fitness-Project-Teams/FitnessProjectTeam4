using FluentValidation;

namespace WorkoutService.Features.Workouts.Queries.GetWorkoutsByPlan
{
    public class GetWorkoutByPlanQueryValidator : AbstractValidator<GetWorkoutByPlanQuery>
    {
        public GetWorkoutByPlanQueryValidator()
        {
            RuleFor(x => x.PlanId)
              .NotEmpty()
              .WithMessage("Workout Plan ID must not be empty.");
        }
    }
}
