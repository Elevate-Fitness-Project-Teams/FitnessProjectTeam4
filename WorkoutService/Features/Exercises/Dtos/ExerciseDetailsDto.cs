namespace WorkoutService.Features.Exercises.Dtos
{
    public record ExerciseDetailsDto(
        Guid Id,
        string Name,
        string EquipmentNeeded,
        List<string> TargetMuscles, 
        string VideoUrl,
        string Difficulty);
}
