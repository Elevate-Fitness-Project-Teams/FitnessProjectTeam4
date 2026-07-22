namespace WorkoutService.Features.Exercises.ViewModels
{
    public record CreateExerciseApiRequest(
          string Name,
          List<string> TargetMuscles,
          string EquipmentNeeded,
          string Description,
          string VideoUrl,
          string Difficulty
    );
}