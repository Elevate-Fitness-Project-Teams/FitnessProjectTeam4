using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgressService.Domian.Aggregates.UserStatistics;
using ProgressService.Infrastructure.Data.Repositories;

namespace ProgressService.Features.Progress.Common.Queries.GetUserStatistics
{
    public class GetUserStatisticsQueryHandler(IGenericRepository<UserStatistic> _repository) : IRequestHandler<GetUserStatisticsQuery, UserStatistic>
    {
        public async Task<UserStatistic> Handle(GetUserStatisticsQuery request, CancellationToken cancellationToken)
        {
            var userStatistic = await _repository.GetAll().AsTracking()
                 .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
            if (userStatistic == null)
                return null;
            return userStatistic;
        }
    }
}
