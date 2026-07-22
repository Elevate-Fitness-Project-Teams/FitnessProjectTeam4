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
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetRecommendationsQuery(
                    mealType, page ?? 1, pageSize ?? 10, maxCalories, minProtein);

                var result = await mediator.Send(query, cancellationToken);
                return Results.Json(result, statusCode: result.StatusCode);
            }).RequireAuthorization();

            
            app.MapGet("/api/v1/nutrition/recommendations/{userId}", async (
                Guid userId,
                [FromQuery] string? mealType,
                [FromQuery] int? maxCalories,
                [FromQuery] float? minProtein,
                [FromQuery] int? page,
                [FromQuery] int? pageSize,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetRecommendationsByUserIdQuery(
                    userId, mealType, maxCalories, minProtein,
                    page ?? 1, pageSize ?? 10);

                var result = await mediator.Send(query, cancellationToken);
                return Results.Json(result, statusCode: result.StatusCode);
            }).RequireAuthorization();
        }
    }
}
