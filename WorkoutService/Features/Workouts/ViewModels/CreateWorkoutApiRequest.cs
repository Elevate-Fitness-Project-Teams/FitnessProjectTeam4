namespace WorkoutService.Features.Workouts.ViewModels
{
    public record CreateWorkoutApiRequest(
           Guid WorkoutPlanId,
           string Name,
           string Category,
           string Difficulty,
           int DurationInMinutes,
           int CaloriesBurn,
           string ImageUrl,
           bool IsPremium,
           int DayNumber
    );


}
