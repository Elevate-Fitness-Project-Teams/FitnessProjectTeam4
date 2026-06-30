namespace WorkoutService.Features.Workouts.ViewModels
{
    public record GetWorkoutRequest(
          string? Category,
          string? Difficulty,
          string? SearchText,
          int Page = 1,
          int PageSize = 10);

}
