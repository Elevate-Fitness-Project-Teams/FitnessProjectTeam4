using AuthService.Common.Api;
using MediatR;
using System.Security.Claims;

namespace AuthService.Features.CompleteProfile;

public static class CompleteProfileEndpoint
{
    public static IEndpointRouteBuilder MapCompleteProfileEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/auth/complete-profile",
            async (ClaimsPrincipal user, IMediator mediator, CancellationToken ct) =>
            {
                var userId = user.GetUserId();
                var result = await mediator.Send(new CompleteProfileCommand(userId), ct);
                return Results.Ok(ApiResponse<object>.Ok(result, "Profile completed."));
            })
            .RequireAuthorization();

        return app;
    }
}
