namespace WorkoutService.Features.Workouts.ViewModels
{
    public record AddExerciseToWorkoutApiRequest(
        Guid WorkoutId,
        Guid ExerciseId,
        int Sets,
        string Reps,
        int RestTimeInSeconds
    );

}
