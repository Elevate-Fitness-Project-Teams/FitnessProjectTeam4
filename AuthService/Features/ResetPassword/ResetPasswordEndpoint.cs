using AuthService.Common.Api;
using MediatR;

namespace AuthService.Features.ResetPassword;

public static class ResetPasswordEndpoint
{
    public static IEndpointRouteBuilder MapResetPasswordEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/auth/reset-password",
            async (ResetPasswordDto dto, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new ResetPasswordCommand(
                    dto.ResetToken, dto.NewPassword, dto.ConfirmPassword), ct);
                return Results.Ok(ApiResponse<object>.Ok(result, "Password reset successfully."));
            })
            .AllowAnonymous();

        return app;
    }
}
