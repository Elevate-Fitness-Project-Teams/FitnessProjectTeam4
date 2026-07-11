
namespace ProgressService.Features.Progress.ViewModels
{
    public record CombinedProgressProfileApiResponse(
         string UserId,
         int CurrentStreak,
         int TotalWorkoutsCompleted,
         double CurrentWeight,
         double TotalWeightLost,
         List<RecentWorkoutLogApiResponse> RecentWorkouts
    );
    public record RecentWorkoutLogApiResponse(Guid LogId, DateTime CompletedAt, int DurationInMinutes, int CaloriesBurned);

}
