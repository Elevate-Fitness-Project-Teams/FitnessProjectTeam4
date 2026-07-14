using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Features.Calculations.Commands.CalculateMetrics;
using MediatR;
using System.Security.Claims;

namespace FitnessCalculationEngine.Features.Calculations;

public static class CalculationsEndpoints
{
    public static IEndpointRouteBuilder MapCalculationsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/fitness").RequireAuthorization();

        group.MapPost("/calculate", async (
            ClaimsPrincipal user,
            IMediator mediator,
            CancellationToken ct) =>
        {
            var userId = user.GetUserId();
            var result = await mediator.Send(new CalculateMetricsCommand(userId), ct);
            return Results.Ok(ApiResponse<object>.Ok(result, "Metrics calculated successfully."));
        });

        return app;
    }
}
