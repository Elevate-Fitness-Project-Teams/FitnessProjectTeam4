namespace WorkoutService.Features.Progress.Dtos
{
    public record ExerciseCompletedDto(Guid ExerciseId, int SetsCompleted, int RepsCompleted, double WeightUsed, bool Completed);
}
