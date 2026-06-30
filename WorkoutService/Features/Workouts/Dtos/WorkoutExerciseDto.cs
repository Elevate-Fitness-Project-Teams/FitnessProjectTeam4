namespace WorkoutService.Features.Workouts.Dtos
{
    public record WorkoutExerciseDto(
        Guid ExerciseId,
        int OrderIndex,
        int SetsDefault,
        string RepsDefault,
        int RestTimeInSeconds,
        string ExerciseName);
}
