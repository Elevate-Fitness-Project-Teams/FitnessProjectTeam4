using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgressService.Common.Responses;
using ProgressService.Domian.Aggregates.WorkoutLogs;
using ProgressService.Features.Progress.Common.Queries.GetStreakByUserId;
using ProgressService.Features.Progress.Common.Queries.GetUserStatistics;
using ProgressService.Features.Progress.Dtos;
using ProgressService.Infrastructure.Data.Repositories;

namespace ProgressService.Features.Progress.Queries.GetUserProfileProgress
{
    public class GetUserProfileProgressQueryHandler(
        IGenericRepository<WorkoutLog> _logRepository,
        IMediator _mediator
        ) : IRequestHandler<GetUserProfileProgressQuery, RequestResult<CombinedProgressProfileDto>>
    {
        public async Task<RequestResult<CombinedProgressProfileDto>> Handle(GetUserProfileProgressQuery request, CancellationToken ct)
        {
            var streak = await _mediator.Send(new GetUserStreakQuery(request.UserId), ct);

            if (streak == null)
                return RequestResult<CombinedProgressProfileDto>.Failure(ErrorCode.StreakNotFound);

            var stats = await _mediator.Send(new GetUserStatisticsQuery(request.UserId), ct);

            if (stats == null)
                return RequestResult<CombinedProgressProfileDto>.Failure(ErrorCode.UserStatisticsNotFound);


            //  Retrieve the last 5 exercises the user completed to display in the Profile
            var recentWorkouts = await _logRepository.GetAll()
                .Where(l => l.UserId == request.UserId)
                .OrderByDescending(l => l.CompletedAt)
                .Take(5)
                .Select(l => new RecentWorkoutLogDto(l.Id, l.CompletedAt, l.DurationInMinutes, l.CaloriesBurned))
                .ToListAsync(ct);

            var profileDto = new CombinedProgressProfileDto(
                request.UserId,
                streak?.CurrentStreak ?? 0,
                stats?.TotalWorkoutsCompleted ?? 0,
                stats?.CurrentWeight ?? 0,
                stats?.TotalWeightLost ?? 0,
                recentWorkouts
            );

            return RequestResult<CombinedProgressProfileDto>.Success(profileDto);
        }
    }
}
