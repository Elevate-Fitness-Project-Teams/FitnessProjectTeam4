using AuthService.Common.Api;
using AuthService.Common.Persistence;
using AuthService.Common.Security;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefreshTokenEntity = AuthService.Domain.Entities.RefreshToken;

namespace AuthService.Features.RefreshToken;

public class RefreshTokenHandler(IRepository repository, IJwtTokenService jwt)
    : IRequestHandler<RefreshTokenCommand, RefreshTokenResultDto>
{
    public async Task<RefreshTokenResultDto> Handle(RefreshTokenCommand cmd, CancellationToken ct)
    {
        var hash = jwt.HashRefreshToken(cmd.RefreshToken);
        var now = DateTime.UtcNow;

        await using var tx = await repository.BeginTransactionAsync(ct);

        var existing = await (
            from r in repository.Query<RefreshTokenEntity>()
            join u in repository.Query<Domain.Entities.User>() on r.UserId equals u.Id
            where r.TokenHash == hash
            select new
            {
                r.Id,
                r.UserId,
                r.FamilyId,
                r.ExpiresAt,
                r.RevokedAt,
                u.Email
            }).FirstOrDefaultAsync(ct);

        if (existing is null)
            throw new AppException(401, ErrorCodes.AUTH_TOKEN_INVALID,
                "Refresh token is invalid or expired.");

        if (existing.RevokedAt != null)
        {
            await repository.Query<RefreshTokenEntity>()
                .Where(r => r.FamilyId == existing.FamilyId && r.RevokedAt == null)
                .ExecuteUpdateAsync(s => s.SetProperty(r => r.RevokedAt, (DateTime?)now), ct);
            await tx.CommitAsync(ct);
            throw new AppException(401, ErrorCodes.AUTH_TOKEN_INVALID,
                "Refresh token is invalid or expired.");
        }

        if (existing.ExpiresAt <= now)
            throw new AppException(401, ErrorCodes.AUTH_TOKEN_EXPIRED,
                "Refresh token has expired.");

        var revoked = await repository.Query<RefreshTokenEntity>()
            .Where(r => r.Id == existing.Id && r.RevokedAt == null)
            .ExecuteUpdateAsync(s => s.SetProperty(r => r.RevokedAt, (DateTime?)now), ct);

        if (revoked == 0)
            throw new AppException(401, ErrorCodes.AUTH_TOKEN_INVALID,
                "Refresh token is invalid or expired.");

        var access = jwt.CreateAccessToken(existing.UserId, existing.Email);
        var (rawRefresh, newHash, newExpires) = jwt.CreateRefreshToken();

        repository.Add(new RefreshTokenEntity
        {
            Id = NewId.NextGuid(),
            UserId = existing.UserId,
            FamilyId = existing.FamilyId,
            TokenHash = newHash,
            ExpiresAt = newExpires,
            CreatedAt = now
        });

        await repository.SaveChangesAsync(ct);
        await tx.CommitAsync(ct);

        return new RefreshTokenResultDto(access.Token, rawRefresh);
    }
}
