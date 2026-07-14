using MediatR;
using ProgressService.Common.Responses;
using ProgressService.Features.Progress.Dtos;

namespace ProgressService.Features.Progress.Queries.GetUserStats
{
    public record GetUserStatsQuery(string UserId) : IRequest<RequestResult<UserStatsDto>>;
}
