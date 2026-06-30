using MediatR;
using ProfileService.Common.GenericResult;
using ProfileService.Features.UploadProfilePicture.DTOs;

namespace ProfileService.Features.UploadProfilePicture.Commands
{
    public record UploadProfilePictureCommand(Guid UserId,UploadProfilePictureDto FileDto) : IRequest<RequestResult<string>>;
}
