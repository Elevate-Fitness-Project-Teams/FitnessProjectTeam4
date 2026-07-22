using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Features.Workouts.Dtos;

namespace WorkoutService.Features.Workouts.Queries.GetWorkoutsByPlan
{
    public record GetWorkoutByPlanQuery(Guid PlanId) : IRequest<RequestResult<List<WorkoutDto>>>;
}
