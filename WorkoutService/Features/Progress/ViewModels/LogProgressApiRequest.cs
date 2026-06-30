using System.ComponentModel.DataAnnotations;

namespace WorkoutService.Features.Progress.ViewModels
{
    public record LogProgressApiRequest(
         Guid WorkoutId,
         Guid SessionId,
         DateTime CompletedAt,
         int Duration,
         int CaloriesBurned,
         string Difficulty,
         string Notes,
         int Rating,
         List<ExerciseCompletedApiItem> ExercisesCompleted
    );

    public record ExerciseCompletedApiItem(
         Guid ExerciseId,
         int SetsCompleted,
         int RepsCompleted,
         double WeightUsed,
         bool Complete
    );
}
