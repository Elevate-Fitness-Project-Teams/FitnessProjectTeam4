using FluentValidation;

namespace WorkoutService.Features.Workouts.Queries.GetWorkoutsByCategory
{
    public class GetWorkoutsByCategoryQueryValidator : AbstractValidator<GetWorkoutsByCategoryQuery>
    {
        public GetWorkoutsByCategoryQueryValidator()
        {
            RuleFor(x => x.CategoryName)
              .NotEmpty()
              .MaximumLength(50)
              .WithMessage("Category name must not be empty and must be less than 50 characters.");
        }
    }
}
