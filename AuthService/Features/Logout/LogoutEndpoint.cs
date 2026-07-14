using AuthService.Common.Api;
using MediatR;
using System.Security.Claims;

namespace AuthService.Features.Logout;

public static class LogoutEndpoint
{
    public static IEndpointRouteBuilder MapLogoutEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/auth/logout",
            async (ClaimsPrincipal user, IMediator mediator, CancellationToken ct) =>
            {
                var userId = user.GetUserId();
                var result = await mediator.Send(new LogoutCommand(userId), ct);
                return Results.Ok(ApiResponse<object>.Ok(result, "Logged out."));
            })
            .RequireAuthorization();

        return app;
    }
}
