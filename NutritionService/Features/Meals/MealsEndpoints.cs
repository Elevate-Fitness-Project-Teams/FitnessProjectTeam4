using MediatR;
using NutritionService.Common;
using NutritionService.Features.Meals.Queries.GetMealDetail;
using System.Threading;

namespace NutritionService.Features.Meals
{
    public class MealsEndpoints : IEndpointDefinition
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/nutrition/meals/{id:guid}", async (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new GetMealDetailQuery(id), cancellationToken);

                return Results.Json(result, statusCode: result.StatusCode);
            }).RequireAuthorization();
        }
    }
}
