using AuthService.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefreshTokenEntity = AuthService.Domain.Entities.RefreshToken;

namespace AuthService.Features.Logout;

public class LogoutHandler(IRepository repository)
    : IRequestHandler<LogoutCommand, LogoutResultDto>
{
    public async Task<LogoutResultDto> Handle(LogoutCommand cmd, CancellationToken ct)
    {
        var now = DateTime.UtcNow;

        await repository.Query<RefreshTokenEntity>()
            .Where(r => r.UserId == cmd.UserId && r.RevokedAt == null)
            .ExecuteUpdateAsync(s => s.SetProperty(r => r.RevokedAt, (DateTime?)now), ct);

        return new LogoutResultDto(true);
    }
}
