using MediatR;
using ProfileService.Common.GenericResult;
using ProfileService.Features.UpdateProfile.ViewModels;

namespace ProfileService.Features.UpdateProfile.Orchestrator
{
    public record UpdateProfileOrchestrator(UpdateProfileRequestViewModel RequestViewModel) : IRequest<ResponseViewModel<bool>>;
}
