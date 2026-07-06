using MediatR;
using ProfileService.Common.GenericResult;
using ProfileService.Features.UpdateSettings.DTOs;

namespace ProfileService.Features.UpdateSettings.Commands
{
    public record UpdateSettingsCommand(Guid UserId, UpdateSettingsDto Dto) : IRequest<RequestResult<bool>>;
}
