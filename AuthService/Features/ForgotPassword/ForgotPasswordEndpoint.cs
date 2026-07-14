using AuthService.Common.Api;
using MediatR;

namespace AuthService.Features.ForgotPassword;

public static class ForgotPasswordEndpoint
{
    public static IEndpointRouteBuilder MapForgotPasswordEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/auth/forgot-password",
            async (ForgotPasswordDto dto, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new ForgotPasswordCommand(dto.Email), ct);
                return Results.Ok(ApiResponse<object>.Ok(result,
                    "If an account exists for this email, a code has been sent."));
            })
            .AllowAnonymous()
            .RequireRateLimiting("forgot-password");

        return app;
    }
}
