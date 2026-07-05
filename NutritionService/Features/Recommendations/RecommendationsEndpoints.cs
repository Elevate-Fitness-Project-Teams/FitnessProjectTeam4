using MediatR;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Features.Recommendations.Queries.GetRecommendations;
using NutritionService.Features.Recommendations.Queries.GetRecommendationsByUserId;

namespace NutritionService.Features.Recommendations
{
    public class RecommendationsEndpoints : IEndpointDefinition
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            
            app.MapGet("/api/v1/nutrition/recommendations", async (
                [FromQuery] string? mealType,
                [FromQuery] int? maxCalories,
                [FromQuery] float? minProtein,
                [FromQuery] int? page,
                [FromQuery] int? pageSize,
                IMediator mediator) =>
            {
                
                var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");

                var query = new GetRecommendationsQuery(
                    userId, mealType, maxCalories, minProtein,
                    page ?? 1, pageSize ?? 10);

                var result = await mediator.Send(query);
                return Results.Json(result, statusCode: result.StatusCode);
            });

            
            app.MapGet("/api/v1/nutrition/recommendations/{userId}", async (
                Guid userId,
                [FromQuery] string? mealType,
                [FromQuery] int? maxCalories,
                [FromQuery] float? minProtein,
                [FromQuery] int? page,
                [FromQuery] int? pageSize,
                IMediator mediator) =>
            {
                var query = new GetRecommendationsByUserIdQuery(
                    userId, mealType, maxCalories, minProtein,
                    page ?? 1, pageSize ?? 10);

                var result = await mediator.Send(query);
                return Results.Json(result, statusCode: result.StatusCode);
            });
        }
    }
}
