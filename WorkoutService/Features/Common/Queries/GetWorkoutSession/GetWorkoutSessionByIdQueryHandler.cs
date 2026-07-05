using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutSessions;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Common.Queries.GetWorkoutSession
{
    public class GetWorkoutSessionByIdQueryHandler(IGenericRepository<WorkoutSession> _repository) : IRequestHandler<GetWorkoutSessionByIdQuery, RequestResult<WorkoutSession>>
    {
        public async Task<RequestResult<WorkoutSession>> Handle(GetWorkoutSessionByIdQuery request, CancellationToken cancellationToken)
        {
            var session = await _repository.GetByIdAsTrackingAsync(request.SessionId, cancellationToken);
            if (session is null)
                return RequestResult<WorkoutSession>.Failure(ErrorCode.WorkoutSessionNotFound, $"Workout session with ID {request.SessionId} not found.");
            return RequestResult<WorkoutSession>.Success(session);
        }
    }
}
