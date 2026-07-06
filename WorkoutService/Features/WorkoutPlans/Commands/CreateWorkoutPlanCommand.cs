using MediatR;
using WorkoutService.Common.Responses;

namespace WorkoutService.Features.WorkoutPlans.Commands
{
    public record CreateWorkoutPlanCommand(
        string Title,
        string Description,
        string Status,
        string Difficulty,
        string Goal,
        string ExternalPlanId,
        Guid UserId,
        string UserTier
    ) : IRequest<RequestResult<Guid>>;
}
