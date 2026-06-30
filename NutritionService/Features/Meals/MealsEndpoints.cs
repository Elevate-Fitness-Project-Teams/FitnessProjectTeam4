using MediatR;
using NutritionService.Common;
using NutritionService.Features.Meals.Queries.GetMealDetail;

namespace NutritionService.Features.Meals
{
    public class MealsEndpoints : IEndpointDefinition
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/nutrition/meals/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetMealDetailQuery(id));

                return Results.Json(result, statusCode: result.StatusCode);
            });
        }
    }
}
