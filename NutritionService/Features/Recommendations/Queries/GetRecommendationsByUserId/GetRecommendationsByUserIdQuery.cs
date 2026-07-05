using MediatR;
using NutritionService.Common;
using NutritionService.Features.Recommendations.DTOs;

namespace NutritionService.Features.Recommendations.Queries.GetRecommendationsByUserId
{
    public record GetRecommendationsByUserIdQuery(
        Guid UserId,
        string? MealType,
        int? MaxCalories,
        float? MinProtein,
        int PageIndex,
        int PageSize
    ) : IRequest<ApiResponse<RecommendationResponseDto>>;
}
