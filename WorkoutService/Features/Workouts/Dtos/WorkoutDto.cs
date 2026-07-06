namespace WorkoutService.Features.Workouts.Dtos
{

    public record WorkoutDto(
           Guid Id,
           string Name,
           string Category,
           string Difficulty,
           int DurationInMinutes
    );
}
