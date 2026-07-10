using MediatR;
using NutritionService.Common;
using NutritionService.Features.Recommendations.DTOs;

namespace NutritionService.Features.Recommendations.Queries.GetRecommendations
{
    public record GetRecommendationsQuery(
         string? MealType,
         int Page,
         int PageSize,
         int? MaxCalories,
         float? MinProtein) : IRequest<Result<RecommendationResponseDto>>;
}
