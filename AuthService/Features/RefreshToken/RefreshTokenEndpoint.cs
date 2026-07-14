using AuthService.Common.Api;
using MediatR;

namespace AuthService.Features.RefreshToken;

public static class RefreshTokenEndpoint
{
    public static IEndpointRouteBuilder MapRefreshTokenEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/auth/refresh-token",
            async (RefreshTokenDto dto, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new RefreshTokenCommand(dto.RefreshToken), ct);
                return Results.Ok(ApiResponse<object>.Ok(result, "Token refreshed."));
            })
            .AllowAnonymous();

        return app;
    }
}
