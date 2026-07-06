namespace ProgressService.Features.Progress.Dtos
{
    public record LogWorkoutProgressResponseDto(Guid LogId, bool StreakUpdated, int CurrentStreak);
}
