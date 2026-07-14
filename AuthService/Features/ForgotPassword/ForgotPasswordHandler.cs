using AuthService.Common.Api;
using AuthService.Common.Email;
using AuthService.Common.Persistence;
using AuthService.Common.Security;
using AuthService.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthService.Features.ForgotPassword;

public class ForgotPasswordHandler(
    IRepository repository,
    IEmailSender emailSender,
    IOptions<OtpOptions> otpOpts)
    : IRequestHandler<ForgotPasswordCommand, ForgotPasswordResultDto>
{
    private readonly OtpOptions _otp = otpOpts.Value;

    public async Task<ForgotPasswordResultDto> Handle(ForgotPasswordCommand cmd, CancellationToken ct)
    {
        var email = cmd.Email.Trim().ToLowerInvariant();
        var now = DateTime.UtcNow;

        var exists = await repository.QueryNoTracking<User>()
            .AnyAsync(u => u.Email == email, ct);

        if (!exists)
            return new ForgotPasswordResultDto(true, _otp.TtlMinutes * 60, _otp.ResendCooldownSeconds);

        var code = OtpHasher.GenerateSixDigitCode();

        await using var tx = await repository.BeginTransactionAsync(
            System.Data.IsolationLevel.Serializable, ct);

        var lastSent = await repository.QueryNoTracking<OtpCode>()
            .Where(o => o.Email == email)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => (DateTime?)o.CreatedAt)
            .FirstOrDefaultAsync(ct);

        if (lastSent.HasValue &&
            (now - lastSent.Value).TotalSeconds < _otp.ResendCooldownSeconds)
        {
            throw new AppException(429, ErrorCodes.RATE_OTP_RESEND_TOO_SOON,
                "Please wait before requesting another code.");
        }

        repository.Add(new OtpCode
        {
            Id = NewId.NextGuid(),
            Email = email,
            CodeHash = OtpHasher.Hash(code),
            ExpiresAt = now.AddMinutes(_otp.TtlMinutes),
            CreatedAt = now,
            IsUsed = false
        });

        await repository.SaveChangesAsync(ct);
        await tx.CommitAsync(ct);

        await emailSender.SendAsync(email, "Your password reset code",
            $"Your verification code is {code}. It expires in {_otp.TtlMinutes} minutes.", ct);

        return new ForgotPasswordResultDto(true, _otp.TtlMinutes * 60, _otp.ResendCooldownSeconds);
    }
}
