using MediatR;
using WorkoutService.Common.Behaviors;
using WorkoutService.Common.Responses;
using WorkoutService.Features.Workouts.Dtos;

namespace WorkoutService.Features.Workouts.Queries.GetWorkouts
{
    public record GetWorkoutsQuery(
          string? Category,
          string? Difficulty,
          string? SearchText,
          int Page = 1,
          int PageSize = 10) : IRequest<RequestResult<PaginatedList<WorkoutDto>>>;
}
