using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Features.FitnessStats.Commands.SubmitStats;
using FitnessCalculationEngine.Features.FitnessStats.DTOs;
using FitnessCalculationEngine.Features.FitnessStats.Queries.GetStats;
using MediatR;
using System.Security.Claims;

namespace FitnessCalculationEngine.Features.FitnessStats;

public static class FitnessStatsEndpoints
{
    public static IEndpointRouteBuilder MapFitnessStatsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/fitness").RequireAuthorization();

        group.MapPost("/weight-goal-activity", async (
            SubmitStatsDto dto,
            ClaimsPrincipal user,
            IMediator mediator,
            CancellationToken ct) =>
        {
            var userId = user.GetUserId();

            var command = new SubmitStatsCommand(
                userId,
                dto.Weight,
                dto.Height,
                dto.Age,
                dto.Gender,
                dto.Goal,
                dto.ActivityLevel
            );

            var id = await mediator.Send(command, ct);

            return Results.Created($"/api/v1/fitness/stats/{id}",
                ApiResponse<object>.Ok(new { id }, "Stats submitted successfully.", 201));
        });

        group.MapGet("/stats", async (
            ClaimsPrincipal user,
            IMediator mediator,
            CancellationToken ct) =>
        {
            var userId = user.GetUserId();
            var result = await mediator.Send(new GetStatsQuery(userId), ct);
            return Results.Ok(ApiResponse<object>.Ok(result));
        });

        return app;
    }
}
