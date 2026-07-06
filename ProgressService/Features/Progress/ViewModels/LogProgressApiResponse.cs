namespace ProgressService.Features.Progress.ViewModels
{
    public record LogProgressApiResponse(Guid LogId, bool StreakUpdated, int CurrentStreak);
}
