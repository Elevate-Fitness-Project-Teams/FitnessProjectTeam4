using MassTransit;
using MediatR;
using ProgressService.Common.Responses;
using ProgressService.Domian.Aggregates.UserStatistics;
using ProgressService.Domian.Aggregates.UserStreaks;
using ProgressService.Domian.Aggregates.WorkoutLogs;
using ProgressService.Features.Progress.Commands.CreateUserStatistic;
using ProgressService.Features.Progress.Commands.CreateUserStreak;
using ProgressService.Features.Progress.Common.Queries.CheckSessionAlreadyLogged;
using ProgressService.Features.Progress.Common.Queries.GetStreakByUserId;
using ProgressService.Features.Progress.Common.Queries.GetUserStatistics;
using ProgressService.Features.Progress.Dtos;
using ProgressService.Infrastructure.Data.Repositories;
using SharedMessages.Messages;
using SharedMessages.Queues;

namespace ProgressService.Features.Progress.Commands.LogWorkoutProgress
{
    public class LogWorkoutProgressOrchestratorHandler(
        IUnitOfWork _unitOfWork,
        IGenericRepository<WorkoutLog> _repository,
        IMediator _mediator,
        ISendEndpointProvider _sendEndpoint,
        ILogger<LogWorkoutProgressOrchestratorHandler> _logger
        ) : IRequestHandler<LogWorkoutProgressOrchestrator, RequestResult<LogWorkoutProgressResponseDto>>
    {
        public async Task<RequestResult<LogWorkoutProgressResponseDto>> Handle(LogWorkoutProgressOrchestrator request, CancellationToken ct)
        {

            bool sessionExists = await _mediator.Send(new CheckSessionAlreadyLoggedQuery(request.SessionId), ct);

            if (!sessionExists)
            {
                _logger.LogWarning("Workout session {SessionId} does not exist for user {UserId}.", request.SessionId, request.UserId);
                return RequestResult<LogWorkoutProgressResponseDto>.Failure(ErrorCode.ProgressCannotBeLogged, "Workout session does not exist.");
            }

            _logger.LogInformation("Ingesting workout performance metrics for session {SessionId} by user {UserId}", request.SessionId, request.UserId);


            var isAlreadyLogged = await _mediator.Send(new CheckSessionAlreadyLoggedQuery(request.SessionId), ct);

            if (isAlreadyLogged)
            {
                _logger.LogWarning("Duplicate progress submission detected for session {SessionId}.", request.SessionId);
                return RequestResult<LogWorkoutProgressResponseDto>.Failure(ErrorCode.ProgressAlreadyLogged, "Workout session already completed and progress logged.");
            }

            try
            {
                var responseDto = await _unitOfWork.ExecuteAsync(async () =>
                {
                    var workoutLog = new WorkoutLog(
                        Guid.NewGuid(),
                        request.WorkoutId,
                        request.SessionId,
                        request.UserId,
                        request.CompletedAt,
                        request.DurationInMinutes,
                        request.CaloriesBurned,
                        request.Difficulty,
                        request.Notes,
                        request.Rating
                    );

                    foreach (var item in request.ExercisesCompleted)
                    {
                        workoutLog.AddCompletedExercise(item.ExerciseId, item.SetsCompleted, item.RepsCompleted, item.WeightUsed, item.Completed);
                    }

                    await _repository.AddAsync(workoutLog, ct);

                    var streak = await _mediator.Send(new GetUserStreakQuery(request.UserId), ct);

                    if (streak == null)
                    {
                        streak = new UserStreak(Guid.NewGuid(), request.UserId);
                        await _mediator.Send(new CreateUserStreakCommand(streak), ct);
                    }

                    bool streakUpdated = streak.UpdateStreak(request.CompletedAt);


                    var stats = await _mediator.Send(new GetUserStatisticsQuery(request.UserId), ct);

                    if (stats == null)
                    {
                        stats = new UserStatistic(Guid.NewGuid(), request.UserId);
                        await _mediator.Send(new CreateUserStatisticCommand(stats), ct);
                    }

                    stats.Accumulate(request.DurationInMinutes, request.CaloriesBurned);

                    // Publish Event
                    var endPoint = await _sendEndpoint.GetSendEndpoint(new Uri($"queue:{QueueNames.WorkoutProgressLogged}"));
                    var message = new WorkoutProgressLoggedMessage(request.SessionId);
                    await endPoint.Send(message);

                    return new LogWorkoutProgressResponseDto(workoutLog.Id, streakUpdated, streak.CurrentStreak);
                }, ct);

                return RequestResult<LogWorkoutProgressResponseDto>.Success(responseDto);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogWarning(ex, "Domain validation error while logging progress for user {UserId}: {Message}", request.UserId, ex.Message);
                return RequestResult<LogWorkoutProgressResponseDto>.Failure(ErrorCode.ValidationError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatal system error during progress record collation context for user {UserId}", request.UserId);
                return RequestResult<LogWorkoutProgressResponseDto>.Failure(ErrorCode.InternalServerError, "An infrastructure ledger writing error occurred.");
            }
        }
    }
}