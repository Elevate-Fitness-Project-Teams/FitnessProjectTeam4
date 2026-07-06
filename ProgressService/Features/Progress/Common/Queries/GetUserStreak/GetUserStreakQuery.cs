using MediatR;
using ProgressService.Common.Responses;
using ProgressService.Domian.Aggregates.UserStreaks;

namespace ProgressService.Features.Progress.Common.Queries.GetStreakByUserId
{
    public record GetUserStreakQuery( string UserId) : IRequest<UserStreak>;
}
