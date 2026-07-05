using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Features.Workouts.Dtos;

namespace WorkoutService.Features.Workouts.Queries.GetWorkoutsByCategory
{
    public record GetWorkoutsByCategoryQuery(string CategoryName) : IRequest<RequestResult<List<WorkoutDto>>>;
}
