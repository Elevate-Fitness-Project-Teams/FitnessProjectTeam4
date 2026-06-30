using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Features.Workouts.Dtos;

namespace WorkoutService.Features.Workouts.Queries.GetWorkoutsByPlan
{
    public record GetWorkoutsByPlanQuery(Guid PlanId) : IRequest<RequestResult<List<WorkoutDto>>>;
}
