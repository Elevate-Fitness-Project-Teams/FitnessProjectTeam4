using MediatR;
using NutritionService.Common;
using NutritionService.Features.Meals.DTOs;

namespace NutritionService.Features.Meals.Queries.GetMealDetail
{
    public record GetMealDetailQuery(Guid MealId) : IRequest<Result<MealDetailDto>>;
}
