using AuthService.Common.Api;
using MediatR;

namespace AuthService.Features.VerifyOtp;

public static class VerifyOtpEndpoint
{
    public static IEndpointRouteBuilder MapVerifyOtpEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/auth/verify-otp",
            async (VerifyOtpDto dto, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new VerifyOtpCommand(dto.Email, dto.Otp), ct);
                return Results.Ok(ApiResponse<object>.Ok(result, "Code verified."));
            })
            .AllowAnonymous();

        return app;
    }
}
