using AuthService.Common.Api;
using MediatR;

namespace AuthService.Features.Register;

public static class RegisterEndpoint
{
    public static IEndpointRouteBuilder MapRegisterEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/auth");

        group.MapPost("/register", async (RegisterDto dto, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new RegisterCommand(
                dto.FirstName, dto.LastName, dto.Email, dto.Password, dto.PhoneNumber), ct);

            return Results.Created($"/api/v1/auth/users/{result.UserId}",
                ApiResponse<object>.Ok(result, "User registered successfully.", 201));
        })
        .AllowAnonymous();

        return app;
    }
}
