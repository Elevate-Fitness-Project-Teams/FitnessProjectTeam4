using WorkoutService.Features.Workouts.Dtos;

namespace WorkoutService.Features.Workouts.ViewModels
{
    public record WorkoutDetailsViewModel
        (Guid Id,
        Guid WorkoutPlanId,
        string Name,
        string Category,
        string Difficulty,
        int DurationInMinutes,
        int CaloriesBurn,
        string ImageUrl,
        bool IsPremium,
        int DayNumber,
        List<WorkoutExerciseDto> WorkoutExercises);

}
