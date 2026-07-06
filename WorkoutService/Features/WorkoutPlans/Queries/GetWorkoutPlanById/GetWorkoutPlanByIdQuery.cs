using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Features.WorkoutPlans.Dtos;

namespace WorkoutService.Features.WorkoutPlans.Queries.GetWorkoutPlanById
{
    public record GetWorkoutPlanByIdQuery(Guid Id) : IRequest<RequestResult<WorkoutPlanDto>>;
}
