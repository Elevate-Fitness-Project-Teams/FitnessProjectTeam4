using AuthService.Common.Api;
using MediatR;

namespace AuthService.Features.Login;

public static class LoginEndpoint
{
    public static IEndpointRouteBuilder MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/auth/login",
            async (LoginDto dto, IMediator mediator, HttpContext http, CancellationToken ct) =>
            {
                var ip = http.Connection.RemoteIpAddress?.ToString();
                var result = await mediator.Send(new LoginCommand(dto.Email, dto.Password, ip), ct);
                return Results.Ok(ApiResponse<object>.Ok(result, "Login successful."));
            })
            .AllowAnonymous()
            .RequireRateLimiting("login");

        return app;
    }
}
