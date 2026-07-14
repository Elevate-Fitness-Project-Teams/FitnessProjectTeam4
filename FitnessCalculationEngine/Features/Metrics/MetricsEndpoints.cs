using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Features.Metrics.Queries.GetMetrics;
using MediatR;
using System.Security.Claims;

namespace FitnessCalculationEngine.Features.Metrics;

public static class MetricsEndpoints
{
    public static IEndpointRouteBuilder MapMetricsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/fitness").RequireAuthorization();

        group.MapGet("/metrics", async (
            ClaimsPrincipal user,
            IMediator mediator,
            CancellationToken ct) =>
        {
            var userId = user.GetUserId();
            var result = await mediator.Send(new GetMetricsQuery(userId), ct);
            return Results.Ok(ApiResponse<object>.Ok(result));
        });

        return app;
    }
}
