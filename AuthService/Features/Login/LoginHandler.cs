using AuthService.Common.Api;
using AuthService.Common.Persistence;
using AuthService.Common.Security;
using AuthService.Domain.Entities;
using MassTransit;
using RefreshTokenEntity = AuthService.Domain.Entities.RefreshToken;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthService.Features.Login;

public class LoginHandler(
    IRepository repository,
    IPasswordHasher hasher,
    IJwtTokenService jwt,
    IOptions<LockoutOptions> lockoutOpts)
    : IRequestHandler<LoginCommand, LoginResultDto>
{
    private readonly LockoutOptions _lockout = lockoutOpts.Value;

    public async Task<LoginResultDto> Handle(LoginCommand cmd, CancellationToken ct)
    {
        var email = cmd.Email.Trim().ToLowerInvariant();
        var now = DateTime.UtcNow;

        await using var tx = await repository.BeginTransactionAsync(ct);

        var user = await repository.Query<User>()
            .Where(u => u.Email == email)
            .Select(u => new
            {
                u.Id,
                u.Email,
                u.PasswordHash,
                u.ProfileCompleted,
                u.IsLockedOut,
                u.LockedUntil
            })
            .FirstOrDefaultAsync(ct);

        if (user is null)
        {
            repository.Add(new LoginAttempt
            {
                Id = NewId.NextGuid(),
                Email = email,
                AttemptedAt = now,
                IsSuccess = false,
                IpAddress = cmd.IpAddress ?? string.Empty
            });
            await repository.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
            throw new AppException(401, ErrorCodes.AUTH_INVALID_CREDENTIALS, "Invalid email or password.");
        }

        if (user.IsLockedOut && user.LockedUntil.HasValue && user.LockedUntil.Value > now)
        {
            throw new AppException(423, ErrorCodes.AUTH_ACCOUNT_LOCKED,
                $"Account is locked. Try again after {user.LockedUntil.Value:O}.");
        }

        var passwordOk = hasher.Verify(cmd.Password, user.PasswordHash);

        repository.Add(new LoginAttempt
        {
            Id = NewId.NextGuid(),
            Email = email,
            AttemptedAt = now,
            IsSuccess = passwordOk,
            IpAddress = cmd.IpAddress ?? string.Empty
        });

        if (!passwordOk)
        {
            await repository.SaveChangesAsync(ct);

            var windowStart = now.AddMinutes(-_lockout.WindowMinutes);
            var recentFailures = await repository.QueryNoTracking<LoginAttempt>()
                .Where(a => a.Email == email && !a.IsSuccess && a.AttemptedAt >= windowStart)
                .CountAsync(ct);

            if (recentFailures >= _lockout.MaxFailures)
            {
                var lockUntil = now.AddMinutes(_lockout.LockMinutes);
                await repository.Query<User>()
                    .Where(u => u.Id == user.Id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(u => u.IsLockedOut, true)
                        .SetProperty(u => u.LockedUntil, lockUntil), ct);
                await tx.CommitAsync(ct);
                throw new AppException(423, ErrorCodes.AUTH_ACCOUNT_LOCKED,
                    $"Too many failed attempts. Account locked until {lockUntil:O}.");
            }

            await tx.CommitAsync(ct);
            throw new AppException(401, ErrorCodes.AUTH_INVALID_CREDENTIALS, "Invalid email or password.");
        }

        if (user.IsLockedOut)
        {
            await repository.Query<User>()
                .Where(u => u.Id == user.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.IsLockedOut, false)
                    .SetProperty(u => u.LockedUntil, (DateTime?)null), ct);
        }

        var access = jwt.CreateAccessToken(user.Id, user.Email);
        var (rawRefresh, refreshHash, refreshExpires) = jwt.CreateRefreshToken();

        repository.Add(new RefreshTokenEntity
        {
            Id = NewId.NextGuid(),
            UserId = user.Id,
            FamilyId = NewId.NextGuid(),
            TokenHash = refreshHash,
            ExpiresAt = refreshExpires,
            CreatedAt = now
        });

        await repository.SaveChangesAsync(ct);
        await tx.CommitAsync(ct);

        return new LoginResultDto(access.Token, rawRefresh, user.ProfileCompleted, false);
    }
}
