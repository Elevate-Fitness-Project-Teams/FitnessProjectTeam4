using MediatR;
using ProfileService.Common.GenericResult;
using ProfileService.Features.UpdateSettings.ViewModels;

namespace ProfileService.Features.UpdateSettings.Orchestrator
{
    public record UpdateSettingsOrchestrator(Guid UserId, UpdateSettingsRequestViewModel RequestViewModel)
        : IRequest<ResponseViewModel<bool>>;
}
