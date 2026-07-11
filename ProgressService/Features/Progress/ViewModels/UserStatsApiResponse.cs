namespace ProgressService.Features.Progress.ViewModels
{
    public record UserStatsApiResponse(
        int TotalWorkouts,
        int TotalCaloriesBurned,
        double CurrentWeight,
        double StartWeight,
        double TotalWeightLost,
        DateTime UpdatedAt
    );
}
