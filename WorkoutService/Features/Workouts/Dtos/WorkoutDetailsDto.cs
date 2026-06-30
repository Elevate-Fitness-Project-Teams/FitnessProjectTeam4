namespace WorkoutService.Features.Workouts.Dtos
{
    public record WorkoutDetailsDto(
        Guid Id,
        Guid WorkoutPlanId,
        string Name,
        string Category,
        string Difficulty,
        int DurationInMinutes,
        int CaloriesBurn,
        string ImageUrl,
        bool IsPremium,
        int DayNumber,
        List<WorkoutExerciseDto> WorkoutExercises);
}
