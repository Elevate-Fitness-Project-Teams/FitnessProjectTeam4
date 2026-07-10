using MediatR;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Features.MealPlans.Queries.BrowseMealPlans;
using NutritionService.Features.MealPlans.Queries.GetMealPlansByCalories;

namespace NutritionService.Features.MealPlans
{
    public class MealPlansEndpoints : IEndpointDefinition
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {

            app.MapGet("/api/v1/nutrition/meal-plans", async (
                 int? pageIndex,
                 int? pageSize,
                 IMediator mediator,
                 CancellationToken cancellationToken) =>
            {
                var query = new BrowseMealPlansQuery(pageIndex ?? 1, pageSize ?? 10);

                var result = await mediator.Send(query, cancellationToken);

                return Results.Json(result, statusCode: result.StatusCode);
            }).RequireAuthorization();



            app.MapGet("/api/v1/nutrition/meal-plans/by-calories", async (
                int? calories,
                int? pageIndex,
                int? pageSize,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
              
                var query = new GetMealPlansByCaloriesQuery(
                    calories,
                    pageIndex ?? 1,
                    pageSize ?? 10);

                var result = await mediator.Send(query, cancellationToken);

                return Results.Json(result, statusCode: result.StatusCode);
            }).RequireAuthorization();
        }
    }
}
