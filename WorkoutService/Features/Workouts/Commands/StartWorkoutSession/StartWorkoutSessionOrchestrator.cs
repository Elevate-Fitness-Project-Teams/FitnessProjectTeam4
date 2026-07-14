using MediatR;
using WorkoutService.Common.Responses;

namespace WorkoutService.Features.Workouts.Commands.StartWorkout
{
    public record StartWorkoutSessionOrchestrator(Guid WorkoutId, Guid UserId) : IRequest<RequestResult<Guid>>; //sessionID
}
