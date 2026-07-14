using AuthService.Common.Api;
using AuthService.Common.Persistence;
using AuthService.Common.Security;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Features.VerifyOtp;

public class VerifyOtpHandler(IRepository repository, IJwtTokenService jwt)
    : IRequestHandler<VerifyOtpCommand, VerifyOtpResultDto>
{
    public async Task<VerifyOtpResultDto> Handle(VerifyOtpCommand cmd, CancellationToken ct)
    {
        var email = cmd.Email.Trim().ToLowerInvariant();
        var codeHash = OtpHasher.Hash(cmd.Otp);
        var now = DateTime.UtcNow;

        var otp = await repository.Query<OtpCode>()
            .Where(o => o.Email == email && o.CodeHash == codeHash && !o.IsUsed)
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefaultAsync(ct);

        if (otp is null)
            throw new AppException(400, ErrorCodes.AUTH_INVALID_OTP, "Invalid verification code.");

        if (otp.ExpiresAt <= now)
            throw new AppException(400, ErrorCodes.AUTH_OTP_EXPIRED, "Verification code has expired.");

        otp.IsUsed = true;
        await repository.SaveChangesAsync(ct);

        var token = jwt.CreateResetToken(email);
        return new VerifyOtpResultDto(token.Token, token.ExpiresAt);
    }
}
