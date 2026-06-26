using MediatR;
using ProfileService.Common.GenericResult;

namespace ProfileService.Features.UpdateProfile.Queries
{
    public record GetUserEmailForCheckQuery(Guid UserId) : IRequest<RequestResult<string>>;
}
