using MediatR;
using ProfileService.Common.GenericResult;
using ProfileService.Features.ViewSettings.ViewModels;

namespace ProfileService.Features.ViewSettings.Orchestrators
{
    public record ViewSettingsOrchestrator(Guid UserId) : IRequest<ResponseViewModel<ViewSettingsResponseViewModel>>;
}
