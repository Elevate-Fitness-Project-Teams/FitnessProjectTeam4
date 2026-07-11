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
                return await _unitOfWork.ExecuteAsync(async () =>
                {
                    // Fake Object Creation for Partial Update
                    var newsession = new WorkoutSession(request.SessionId);

                    newsession.CompleteSession(); 

                    var propToUpdate =new [] { nameof(WorkoutSession.Status), nameof(WorkoutSession.EndTime)};

                    _repository.SaveInclude(newsession, propToUpdate);

                    _logger.LogInformation("Successfully completed database partial update for session {SessionId}", request.SessionId);

                    return RequestResult<Unit>.Success(Unit.Value);
                }, cancellationToken);
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
