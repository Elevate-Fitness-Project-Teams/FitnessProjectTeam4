using MediatR;

namespace AuthService.Features.Logout;

public record LogoutResultDto(bool LoggedOut);

public record LogoutCommand(Guid UserId) : IRequest<LogoutResultDto>;
