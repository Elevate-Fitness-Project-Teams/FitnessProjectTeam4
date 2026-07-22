namespace WorkoutService.Features.Exercises.Dtos
{
    public record ExerciseDto(
        Guid Id,
        string Name,
        string Description,
        string EquipmentNeeded,
        List<string> TargetMuscles, 
        string VideoUrl,
        string Difficulty
    );
}
