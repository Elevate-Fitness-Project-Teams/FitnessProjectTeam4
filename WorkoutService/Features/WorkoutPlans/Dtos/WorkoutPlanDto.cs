namespace WorkoutService.Features.WorkoutPlans.Dtos
{
    public record WorkoutPlanDto(
        Guid Id,
        string Title,
        string Description,
        string Difficulty,
        string Goal,
        string Status,
        string UserTier);
}
