using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedMessages.Messages;
using SharedMessages.Queues;
using WorkoutService.Common.Exceptions;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutSessions;
using WorkoutService.Features.Common.Queries.CheckWorkoutExists;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Workouts.Commands.StartWorkout
{
    public class StartWorkoutSessionOrchestratorHandler(
    IGenericRepository<WorkoutSession> _repository,
    IUnitOfWork _unitOfWork,
    IMediator _mediator,
    ILogger<StartWorkoutSessionOrchestratorHandler> _logger,
    ISendEndpointProvider _sendEndpoint)
    : IRequestHandler<StartWorkoutSessionOrchestrator, RequestResult<Guid>>
    {
        public async Task<RequestResult<Guid>> Handle(StartWorkoutSessionOrchestrator request, CancellationToken ct)
        {
            _logger.LogInformation("Starting workout session for WorkoutId: {WorkoutId}, UserId: {UserId}", request.WorkoutId, request.UserId);

            try
            {
                bool workoutExists = await _mediator.Send(new WorkoutExistsQuery(request.WorkoutId), ct);

                if (!workoutExists)
                {
                    _logger.LogWarning(
                        "Workout with Id {WorkoutId} was not found.",
                        request.WorkoutId);

                    return RequestResult<Guid>.Failure(
                        ErrorCode.WorkoutNotFound,
                        $"Workout with ID '{request.WorkoutId}' does not exist.");
                }

                bool hasActiveSession = await _repository.AnyAsync(
                    s => s.UserId == request.UserId &&
                         s.Status == WorkoutSessionStatus.Active,
                    ct);

                if (hasActiveSession)
                {
                    _logger.LogWarning(
                        "User {UserId} already has an active workout session.",
                        request.UserId);

                    return RequestResult<Guid>.Failure(
                        ErrorCode.WorkoutSessionAlreadyStarted,
                        "You already have an active workout session.");
                }

                var sessionId = await _unitOfWork.ExecuteAsync(async () =>
                {
                    var session = WorkoutSession.Start(
                        request.UserId,
                        request.WorkoutId);

                    await _repository.AddAsync(session, ct);

                    var endpoint = await _sendEndpoint.GetSendEndpoint(
                        new Uri($"queue:{QueueNames.SessionStarted}"));

                    await endpoint.Send(new SessionStartedMessage
                    {
                        SessionId = session.Id,
                        WorkoutId = session.WorkoutId,
                        UserId = session.UserId,
                        StartTime = session.StartTime,
                        Status = session.Status
                    });

                    _logger.LogInformation(
                        "Published SessionStarted event for SessionId {SessionId}",
                        session.Id);

                    return session.Id;
                }, ct);

                _logger.LogInformation(
                    "Workout session {SessionId} started successfully.",
                    sessionId);

                return RequestResult<Guid>.Success(sessionId);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Business rule violation while starting workout for UserId {UserId}",
                    request.UserId);

                return RequestResult<Guid>.Failure(ex.ErrorCode, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Concurrent active session detected for UserId {UserId}",
                    request.UserId);

                return RequestResult<Guid>.Failure(
                    ErrorCode.WorkoutSessionAlreadyStarted,
                    "You already have an active workout session.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unexpected error while starting workout session for UserId {UserId}",
                    request.UserId);

                return RequestResult<Guid>.Failure(
                    ErrorCode.InternalServerError,
                    "An unexpected error occurred while starting the workout session.");
            }
        }
    }
}
