using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutSessions;

namespace WorkoutService.Features.Common.Queries.GetWorkoutSession
{
    public record GetWorkoutSessionByIdQuery(Guid SessionId) : IRequest<RequestResult<WorkoutSession>>;
}
