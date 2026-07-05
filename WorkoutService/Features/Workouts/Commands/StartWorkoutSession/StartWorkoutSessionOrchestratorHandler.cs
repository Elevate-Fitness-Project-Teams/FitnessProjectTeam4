using MassTransit;
using MediatR;
using SharedMessages.Messages;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutSessions;
using WorkoutService.Features.Workouts.Queries.GetWorkoutById;
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


            var workoutExists = await _mediator.Send(new GetWorkoutByIdQuery(request.WorkoutId), ct);

            if (!workoutExists.IsSuccess)
            {
                _logger.LogWarning("Workout with Id {WorkoutId} was not found.", request.WorkoutId);

                return RequestResult<Guid>.Failure(ErrorCode.WorkoutNotFound, $"Workout with ID '{request.WorkoutId}' does not exist.");
            }

            var hasActiveSession = await _repository.AnyAsync(s => s.UserId == request.UserId && s.Status == WorkoutSessionStatus.Active, ct);

            if (hasActiveSession)
            {
                _logger.LogWarning("User {UserId} already has an active workout session.", request.UserId);

                return RequestResult<Guid>.Failure(ErrorCode.WorkoutAlreadyStarted, "You already have an active workout session.");
            }

            var sessionId = await _unitOfWork.ExecuteAsync(async () =>
            {
                var session = WorkoutSession.Start(
                    request.UserId,
                    request.WorkoutId);

                await _repository.AddAsync(session, ct);

                // Publish Event
                var endPoint = await _sendEndpoint.GetSendEndpoint(new Uri("queue:session-Started"));
                await endPoint.Send(new SessionStartedMessage
                {
                    SessionId = session.Id,
                    WorkoutId = session.WorkoutId,
                    UserId = session.UserId,
                    StartTime = session.StartTime,
                    Status = session.Status
                });


                return session.Id;
            }, ct);

            _logger.LogInformation("Workout session {SessionId} started successfully.", sessionId);

            return RequestResult<Guid>.Success(sessionId);
        }
    }
}
