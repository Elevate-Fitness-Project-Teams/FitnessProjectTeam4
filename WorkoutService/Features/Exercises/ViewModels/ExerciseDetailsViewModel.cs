namespace WorkoutService.Features.Exercises.ViewModels
{
    public record ExerciseDetailsViewModel(
        Guid Id,
        string Name,
        string EquipmentNeeded,
        List<string> TargetMuscles,
        string VideoUrl,
        string Difficulty);
}
