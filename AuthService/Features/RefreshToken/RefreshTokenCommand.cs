using MediatR;

namespace AuthService.Features.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<RefreshTokenResultDto>;
