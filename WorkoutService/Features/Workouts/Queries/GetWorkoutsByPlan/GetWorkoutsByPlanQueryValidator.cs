using FluentValidation;

namespace WorkoutService.Features.Workouts.Queries.GetWorkoutsByPlan
{
    public class GetWorkoutsByPlanQueryValidator : AbstractValidator<GetWorkoutsByPlanQuery>
    {
        public GetWorkoutsByPlanQueryValidator()
        {
            RuleFor(x => x.PlanId)
              .NotEmpty()
              .WithMessage("Workout Plan ID must not be empty.");
        }
    }
}
