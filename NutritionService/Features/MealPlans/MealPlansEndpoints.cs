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
                [FromQuery] int? pageIndex,
                [FromQuery] int? pageSize,
                IMediator mediator) =>
            {
                var query = new BrowseMealPlansQuery(pageIndex ?? 1, pageSize ?? 10);
                var result = await mediator.Send(query);
                return Results.Json(result, statusCode: result.StatusCode);
            });

            
            app.MapGet("/api/v1/nutrition/meal-plans/by-calories", async (
                [FromQuery] int? calories,
                IMediator mediator) =>
            {
                if (calories is null)
                    throw new ArgumentException("VAL_REQUIRED_FIELD");

                var query = new GetMealPlansByCaloriesQuery(calories.Value);
                var result = await mediator.Send(query);
                return Results.Json(result, statusCode: result.StatusCode);
            });
        }
    }
}
