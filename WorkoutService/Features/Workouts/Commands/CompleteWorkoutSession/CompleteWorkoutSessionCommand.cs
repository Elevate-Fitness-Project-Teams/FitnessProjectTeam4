using MediatR;
using WorkoutService.Common.Responses;

namespace WorkoutService.Features.Workouts.Commands.CompleteWorkoutSession
{
    public record CompleteWorkoutSessionCommand( Guid SessionId) : IRequest<RequestResult<Unit>>;

}
