namespace ProgressService.Features.Progress.Dtos
{
    public record CombinedProgressProfileDto(
        string UserId,
        int CurrentStreak,
        int TotalWorkoutsCompleted,
        double CurrentWeight,
        double TotalWeightLost,
        List<RecentWorkoutLogDto> RecentWorkouts
    );

    public record RecentWorkoutLogDto(Guid LogId, DateTime CompletedAt, int DurationInMinutes, int CaloriesBurned);
}
