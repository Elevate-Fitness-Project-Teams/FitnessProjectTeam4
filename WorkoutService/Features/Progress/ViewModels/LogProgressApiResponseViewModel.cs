namespace WorkoutService.Features.Progress.ViewModels
{
    public record LogProgressApiResponseViewModel(Guid LogId, bool StreakUpdated, int CurrentStreak);
}
