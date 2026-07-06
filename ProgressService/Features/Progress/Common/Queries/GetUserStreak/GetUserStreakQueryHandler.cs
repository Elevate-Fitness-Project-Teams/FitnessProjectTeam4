using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgressService.Domian.Aggregates.UserStreaks;
using ProgressService.Infrastructure.Data.Repositories;

namespace ProgressService.Features.Progress.Common.Queries.GetStreakByUserId
{
    public class GetUserStreakQueryHandler(IGenericRepository<UserStreak> _streakRepository) : IRequestHandler<GetUserStreakQuery, UserStreak>
    {
        public async Task<UserStreak> Handle(GetUserStreakQuery request, CancellationToken cancellationToken)
        {
            var streak = await _streakRepository.GetAll().AsTracking()
                .FirstOrDefaultAsync(s => s.UserId == request.UserId, cancellationToken);
            if (streak == null)
                return null;
            return streak;
        }
    }
}
