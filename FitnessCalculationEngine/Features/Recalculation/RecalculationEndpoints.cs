using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Features.Recalculation.Commands.Recalculate;
using FitnessCalculationEngine.Features.Recalculation.DTOs;
using MediatR;
using System.Security.Claims;

namespace FitnessCalculationEngine.Features.Recalculation;

public static class RecalculationEndpoints
{
    public static IEndpointRouteBuilder MapRecalculationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/fitness").RequireAuthorization();

        group.MapPut("/recalculate", async (
            RecalculateRequestDto? dto,
            ClaimsPrincipal user,
            IMediator mediator,
            CancellationToken ct) =>
        {
            var userId = user.GetUserId();
            var result = await mediator.Send(new RecalculateCommand(
                userId, dto?.Reason, dto?.NewWeight, dto?.TriggeredBy), ct);
            return Results.Ok(ApiResponse<object>.Ok(result, "Metrics recalculated."));
        });

        return app;
    }
}
