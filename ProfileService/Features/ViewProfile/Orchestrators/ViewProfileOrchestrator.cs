using MediatR;
using ProfileService.Common.GenericResult;
using ProfileService.Features.ViewProfile.ViewModels;

namespace ProfileService.Features.ViewProfile.Orchestrators
{
    public record ViewProfileOrchestrator(Guid UserId) : IRequest<RequestResult<ProfileViewModel>>;
}
