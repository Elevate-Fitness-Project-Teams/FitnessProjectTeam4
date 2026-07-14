using MediatR;

namespace AuthService.Features.Login;

public record LoginCommand(string Email, string Password, string? IpAddress)
    : IRequest<LoginResultDto>;
