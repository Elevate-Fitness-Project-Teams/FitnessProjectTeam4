using MediatR;

namespace AuthService.Features.VerifyOtp;

public record VerifyOtpCommand(string Email, string Otp) : IRequest<VerifyOtpResultDto>;
