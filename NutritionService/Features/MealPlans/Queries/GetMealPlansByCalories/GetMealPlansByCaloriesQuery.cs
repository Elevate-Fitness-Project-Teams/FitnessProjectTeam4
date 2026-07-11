using MediatR;
using NutritionService.Common;
using NutritionService.Features.MealPlans.DTOs;

namespace NutritionService.Features.MealPlans.Queries.GetMealPlansByCalories
{
    public record GetMealPlansByCaloriesQuery(int? Calories, int PageIndex, int PageSize)
        : IRequest<Result<PagedResult<MealPlanByCaloriesDto>>>;
}
