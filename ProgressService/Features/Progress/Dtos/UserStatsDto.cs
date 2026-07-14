namespace ProgressService.Features.Progress.Dtos
{
    public record UserStatsDto(
        int TotalWorkouts,
        int TotalCaloriesBurned,
        double CurrentWeight,
        double StartWeight,
        double TotalWeightLost,
        DateTime UpdatedAt
    );
}
