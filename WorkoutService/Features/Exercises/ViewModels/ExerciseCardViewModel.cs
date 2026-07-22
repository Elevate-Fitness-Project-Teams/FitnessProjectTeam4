namespace WorkoutService.Features.Exercises.ViewModels
{
    public record ExerciseCardViewModel(
        Guid Id,
        string Name,
        string Description,
        string EquipmentNeeded,
        List<string> TargetMuscles,
        string VideoUrl,
        string Difficulty
    );
}
