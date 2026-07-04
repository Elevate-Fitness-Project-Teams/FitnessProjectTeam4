using MediatR;
using ProfileService.Common.GenericResult;
using ProfileService.Features.ViewSettings.DTOs;

namespace ProfileService.Features.ViewSettings.Queries
{
    public record GetUserSettingsQuery(Guid UserId) : IRequest<RequestResult<UserSettingsDto>>;
}
