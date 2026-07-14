using AuthService.Common.Api;
using AuthService.Common.Messaging.Events;
using AuthService.Common.Persistence;
using AuthService.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Features.CompleteProfile;

public class CompleteProfileHandler(IRepository repository, IPublishEndpoint publisher)
    : IRequestHandler<CompleteProfileCommand, CompleteProfileResultDto>
{
    public async Task<CompleteProfileResultDto> Handle(CompleteProfileCommand cmd, CancellationToken ct)
    {
        var user = await repository.Query<User>()
            .FirstOrDefaultAsync(u => u.Id == cmd.UserId, ct)
            ?? throw new AppException(401, ErrorCodes.AUTH_TOKEN_INVALID, "User not found for token.");

        if (user.ProfileCompleted)
            return new CompleteProfileResultDto(true);

        user.ProfileCompleted = true;
        await repository.SaveChangesAsync(ct);

        await publisher.Publish(new UserRegisteredEvent(
            user.Id, user.Email, user.FirstName, user.LastName, user.PhoneNumber, DateTime.UtcNow), ct);

        return new CompleteProfileResultDto(true);
    }
}
