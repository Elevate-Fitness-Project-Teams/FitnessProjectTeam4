using MediatR;
using NutritionService.Common;
using NutritionService.Features.MealPlans.DTOs;

namespace NutritionService.Features.MealPlans.Queries.GetMealPlansByCalories
{
    public record GetMealPlansByCaloriesQuery(int Calories) 
        : IRequest<ApiResponse<List<MealPlanByCaloriesDto>>>;
}
