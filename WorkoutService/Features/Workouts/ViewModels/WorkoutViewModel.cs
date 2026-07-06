namespace WorkoutService.Features.Workouts.ViewModels
{
    public record WorkoutViewModel(
          Guid Id,
           string Name,
           string Category,
           string Difficulty,
           int DurationInMinutes
    );

}
