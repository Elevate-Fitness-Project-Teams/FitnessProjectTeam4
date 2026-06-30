namespace WorkoutService.Features.Exercises.ViewModels
{
    public record ExerciseCardViewModel(
        Guid Id,
        string Name,
        string BodyPart,
        string Equipment,
        string TargetMusclesJson, 
        string ThumbnailUrl,
        string Difficulty);
}
