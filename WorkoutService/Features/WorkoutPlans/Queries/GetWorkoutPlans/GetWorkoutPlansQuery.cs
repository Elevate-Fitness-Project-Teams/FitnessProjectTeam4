using MediatR;
using WorkoutService.Common.Behaviors;
using WorkoutService.Common.Responses;
using WorkoutService.Features.WorkoutPlans.Dtos;

namespace WorkoutService.Features.WorkoutPlans.Queries.GetWorkoutPlans
{
    public record GetWorkoutPlansQuery(
        string? Goal,
        int PageNumber = 1,
        int PageSize = 10) : IRequest<RequestResult<PaginatedList<WorkoutPlanDto>>>;
}
