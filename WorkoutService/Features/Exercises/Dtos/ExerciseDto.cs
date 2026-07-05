namespace WorkoutService.Features.Exercises.Dtos
{
    public record ExerciseDto(
        Guid Id,
        string Name,
        string BodyPart,
        string Equipment,
        string TargetMuscles, 
        string ThumbnailUrl,
        string Difficulty);
}
