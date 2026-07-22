namespace ProgressService.Features.Progress.Dtos
{
    public class UserStatsDto
    {
        public int TotalWorkoutsCompleted { get; init; }
        public int TotalCaloriesBurned { get; init; }
        public int TotalMinutesTrained { get; init; }
        public double CurrentWeight { get; init; }
        public double StartWeight { get; init; }
        public double TotalWeightLost { get; init; }
    }
}
