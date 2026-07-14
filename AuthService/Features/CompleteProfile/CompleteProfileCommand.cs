using MediatR;

namespace AuthService.Features.CompleteProfile;

public record CompleteProfileResultDto(bool ProfileCompleted);

public record CompleteProfileCommand(Guid UserId) : IRequest<CompleteProfileResultDto>;
