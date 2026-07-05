using MediatR;
using NutritionService.Common;
using NutritionService.Features.MealPlans.DTOs;

namespace NutritionService.Features.MealPlans.Queries.BrowseMealPlans
{
    public record BrowseMealPlansQuery(int PageIndex, int PageSize)
       : IRequest<ApiResponse<PagedResult<BrowseMealPlanDto>>>;
}
