using MediatR;
using ProgressService.Domian.Aggregates.UserStatistics;

namespace ProgressService.Features.Progress.Commands.CreateUserStatistic
{
    public record CreateUserStatisticCommand(UserStatistic Statistic) : IRequest;
}
