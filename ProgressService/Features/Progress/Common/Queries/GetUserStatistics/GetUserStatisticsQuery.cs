using MediatR;
using ProgressService.Domian.Aggregates.UserStatistics;

namespace ProgressService.Features.Progress.Common.Queries.GetUserStatistics
{
    public record GetUserStatisticsQuery(string UserId) : IRequest<UserStatistic>;
}
