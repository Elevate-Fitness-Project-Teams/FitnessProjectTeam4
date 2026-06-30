using MediatR;
using ProfileService.Common.GenericResult;

namespace ProfileService.Features.UpdateProfile.Commands
{
    public record UpdateProfileCommand(Guid UserId,string? FirstName,string? LastName,string? PhoneNumber) : IRequest<RequestResult<bool>>;
}
