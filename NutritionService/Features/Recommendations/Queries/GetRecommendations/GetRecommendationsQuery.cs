using MediatR;
using NutritionService.Common;
using NutritionService.Features.Recommendations.DTOs;

namespace NutritionService.Features.Recommendations.Queries.GetRecommendations
{
    public record GetRecommendationsQuery(
        Guid UserId,
        string? MealType,
        int? MaxCalories,
        float? MinProtein,
        int PageIndex,
        int PageSize
    ) : IRequest<ApiResponse<RecommendationResponseDto>>;
}
