using MediatR;
using ProfileService.Common.GenericResult;

namespace ProfileService.Features.UploadProfilePicture.Queries
{
    public record CheckProfileExistsForUploadQuery(Guid UserId) : IRequest<RequestResult<bool>>;
}
