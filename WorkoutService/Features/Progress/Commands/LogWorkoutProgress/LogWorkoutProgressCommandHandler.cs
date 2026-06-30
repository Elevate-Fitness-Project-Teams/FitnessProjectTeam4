using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutLogs;
using WorkoutService.Features.Common.Queries.GetWorkoutSession;
using WorkoutService.Features.Progress.Dtos;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Progress.Commands.LogWorkoutProgress
{
    public class LogWorkoutProgressCommandHandler(
        IUnitOfWork _unitOfWork,
        IGenericRepository<WorkoutLog> _logRepository,
        IMediator _mediator,
        ILogger<LogWorkoutProgressCommandHandler> _logger
        ) : IRequestHandler<LogWorkoutProgressCommand, RequestResult<LogWorkoutProgressResponseDto>>
    {
        public async Task<RequestResult<LogWorkoutProgressResponseDto>> Handle(LogWorkoutProgressCommand request, CancellationToken ct)
        {
            _logger.LogInformation("Ingesting workout execution logging for session: {SessionId} by user: {UserId}", request.SessionId, request.UserId);

            try
            {
                var resultData = await _unitOfWork.ExecuteAsync(async () =>
                {
                    var session = await _mediator.Send(new GetWorkoutSessionByIdQuery(request.SessionId), ct);

                    if (!session.IsSuccess)
                        throw new KeyNotFoundException($"Workout execution session '{request.SessionId}' was not found in the ingestion ledger.");

                    if (session.Data.Status == "Completed")
                        throw new InvalidOperationException("Workout session already completed. Cannot process progress logs multiple times.");

                    session.Data.CompleteSession();

                    var workoutLog = new WorkoutLog(
                        request.WorkoutId,
                        request.SessionId,
                        request.UserId,
                        request.CompletedAt,
                        request.Duration,
                        request.CaloriesBurned,
                        request.Difficulty,
                        request.Notes,
                        request.Rating
                    );

                    foreach (var item in request.ExercisesCompleted)
                    {
                        workoutLog.AddCompletedExercise(item.ExerciseId, item.SetsCompleted, item.RepsCompleted, item.WeightUsed, item.Completed);
                    }

                    await _logRepository.AddAsync(workoutLog, ct);

                    bool streakUpdated = true;
                    int currentStreak = 8;

                    return new LogWorkoutProgressResponseDto(workoutLog.Id, streakUpdated, currentStreak);
                }, ct);

                return RequestResult<LogWorkoutProgressResponseDto>.Success(resultData);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Session lookup validation failure during log ingest: {Message}", ex.Message);
                return RequestResult<LogWorkoutProgressResponseDto>.Failure(ErrorCode.WorkoutSessionNotFound, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Workflow conflict or duplicate submission: {Message}", ex.Message);
                return RequestResult<LogWorkoutProgressResponseDto>.Failure(ErrorCode.ValidationError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Transaction aborted during performance progress collection for user {UserId}", request.UserId);
                return RequestResult<LogWorkoutProgressResponseDto>.Failure(ErrorCode.InternalServerError, "An internal ingestion ledger error occurred.");
            }
        }
    }
}
