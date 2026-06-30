namespace WorkoutService.Features.WorkoutPlans.ViewModels
{
    public record CreateWorkoutPlanRequest(
           string Title,
           string Description,
           string Status,
           string Difficulty,
           string Goal,
           string ExternalPlanId,
           Guid UserId,
           string UserTier
    );
}
