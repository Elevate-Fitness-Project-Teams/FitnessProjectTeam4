using MediatR;

namespace AuthService.Features.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest<ForgotPasswordResultDto>;
