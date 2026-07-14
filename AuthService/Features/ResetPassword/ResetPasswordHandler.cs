using AuthService.Common.Api;
using AuthService.Common.Persistence;
using AuthService.Common.Security;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefreshTokenEntity = AuthService.Domain.Entities.RefreshToken;

namespace AuthService.Features.ResetPassword;

public class ResetPasswordHandler(
    IRepository repository,
    IJwtTokenService jwt,
    IPasswordHasher hasher)
    : IRequestHandler<ResetPasswordCommand, ResetPasswordResultDto>
{
    public async Task<ResetPasswordResultDto> Handle(ResetPasswordCommand cmd, CancellationToken ct)
    {
        if (cmd.NewPassword != cmd.ConfirmPassword)
            throw new AppException(400, ErrorCodes.AUTH_PASSWORD_MISMATCH, "Passwords do not match.");

        var email = jwt.ValidateResetToken(cmd.ResetToken)
            ?? throw new AppException(400, ErrorCodes.AUTH_RESET_TOKEN_INVALID,
                "Reset token is invalid or expired.");

        email = email.Trim().ToLowerInvariant();
        var now = DateTime.UtcNow;
        var newHash = hasher.Hash(cmd.NewPassword);

        await using var tx = await repository.BeginTransactionAsync(ct);

        var userId = await repository.QueryNoTracking<User>()
            .Where(u => u.Email == email)
            .Select(u => (Guid?)u.Id)
            .FirstOrDefaultAsync(ct);

        if (userId is null)
            throw new AppException(400, ErrorCodes.AUTH_RESET_TOKEN_INVALID,
                "Reset token is invalid or expired.");

        await repository.Query<User>()
            .Where(u => u.Id == userId.Value)
            .ExecuteUpdateAsync(s => s.SetProperty(u => u.PasswordHash, newHash), ct);

        await repository.Query<RefreshTokenEntity>()
            .Where(r => r.UserId == userId.Value && r.RevokedAt == null)
            .ExecuteUpdateAsync(s => s.SetProperty(r => r.RevokedAt, (DateTime?)now), ct);

        await tx.CommitAsync(ct);

        return new ResetPasswordResultDto(true);
    }
}
