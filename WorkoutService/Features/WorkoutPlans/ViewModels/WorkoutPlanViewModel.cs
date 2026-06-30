namespace WorkoutService.Features.WorkoutPlans.ViewModels
{
    public record WorkoutPlanViewModel(
        Guid Id,
        string Title,
        string Description,
        string Difficulty,
        string Goal,
        string Status,
        string UserTier);
}

