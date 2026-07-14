using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Domain.Enums;
using FitnessCalculationEngine.Features.Plans.Commands.AssignPlan;
using FitnessCalculationEngine.Features.Plans.Queries.GetPlanConfig;
using FitnessCalculationEngine.Features.Plans.Queries.GetPlanConfigs;
using MediatR;
using System.Security.Claims;

namespace FitnessCalculationEngine.Features.Plans;

public static class PlansEndpoints
{
    public static IEndpointRouteBuilder MapPlansEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/fitness").RequireAuthorization();

        group.MapPost("/assign-plan", async (
            ClaimsPrincipal user,
            IMediator mediator,
            CancellationToken ct) =>
        {
            var userId = user.GetUserId();
            var result = await mediator.Send(new AssignPlanCommand(userId), ct);
            return Results.Ok(ApiResponse<object>.Ok(result, "Plan assigned successfully."));
        });

        group.MapGet("/plan-configs", async (
            IMediator mediator,
            CancellationToken ct,
            Goal? goal = null,
            FitnessStatus? status = null,
            int page = 1,
            int pageSize = 20) =>
        {
            var result = await mediator.Send(new GetPlanConfigsQuery(goal, status, page, pageSize), ct);
            return Results.Ok(ApiResponse<object>.Ok(result, "Plan configurations retrieved."));
        });

        group.MapGet("/plans/{planId}", async (
            string planId,
            IMediator mediator,
            CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetPlanConfigQuery(planId), ct);
            return Results.Ok(ApiResponse<object>.Ok(result, "Plan retrieved."));
        });

        return app;
    }
}
