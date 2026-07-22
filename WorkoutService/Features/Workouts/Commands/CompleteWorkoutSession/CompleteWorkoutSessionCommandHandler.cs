using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutSessions;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Workouts.Commands.CompleteWorkoutSession
{
    public class CompleteWorkoutSessionCommandHandler(
        IGenericRepository<WorkoutSession> _repository,
        IUnitOfWork _unitOfWork,
        ILogger<CompleteWorkoutSessionCommandHandler> _logger
        ) : IRequestHandler<CompleteWorkoutSessionCommand, RequestResult<Unit>>
    {
        public async Task<RequestResult<Unit>> Handle(CompleteWorkoutSessionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing partial update for workout session {SessionId} to status 'Completed'", request.SessionId);

            bool sessionExists = await _repository.AnyAsync(x => x.Id == request.SessionId, cancellationToken);
            if (!sessionExists)
                return RequestResult<Unit>.Failure(ErrorCode.WorkoutSessionNotFound, $"Workout session with ID {request.SessionId} not found.");

            try
            {
                WorkoutSession newsession = await _repository.GetByIdAsTrackingAsync(request.SessionId, cancellationToken);

                newsession.CompleteSession();

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully completed database partial update for session {SessionId}", request.SessionId);

                return RequestResult<Unit>.Success(Unit.Value);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Domain validation rule broken during partial session state update for {SessionId}: {Message}", request.SessionId, ex.Message);
                return RequestResult<Unit>.Failure(ErrorCode.ValidationError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to perform atomic ledger update for session state transition on {SessionId}", request.SessionId);
                return RequestResult<Unit>.Failure(ErrorCode.InternalServerError, "An infrastructure ledger writing error occurred.");
            }
        }
    }
}
