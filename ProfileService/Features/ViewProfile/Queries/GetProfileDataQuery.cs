using MediatR;
using ProfileService.Common.GenericResult;
using ProfileService.Features.ViewProfile.DTOs;

namespace ProfileService.Features.ViewProfile.Queries
{
    public record GetProfileDataQuery(Guid UserId) : IRequest<RequestResult<ProfileDataDto>>;
}
