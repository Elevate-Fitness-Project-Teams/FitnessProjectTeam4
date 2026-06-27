using MediatR;
using ProfileService.Common.GenericResult;
using ProfileService.Features.UploadProfilePicture.ViewModels;

namespace ProfileService.Features.UploadProfilePicture.Orchestrators
{
    public record UploadProfilePictureOrchestrator(UploadProfilePictureRequestViewModel RequestViewModel)
        : IRequest<ResponseViewModel<UploadProfilePictureResponseViewModel>>;
}
