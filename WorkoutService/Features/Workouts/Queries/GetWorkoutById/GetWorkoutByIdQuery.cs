using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Features.Workouts.Dtos;

namespace WorkoutService.Features.Workouts.Queries.GetWorkoutById
{
    public record GetWorkoutByIdQuery(Guid Id) : IRequest<RequestResult<WorkoutDetailsDto>>;
}
