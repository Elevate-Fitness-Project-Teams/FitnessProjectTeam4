using MediatR;
using WorkoutService.Common.Responses;

namespace WorkoutService.Features.Workouts.Commands.StartWorkout
{
    public record StartWorkoutCommand(Guid WorkoutId, Guid UserId) : IRequest<RequestResult<Guid>>; //sessionID
}
