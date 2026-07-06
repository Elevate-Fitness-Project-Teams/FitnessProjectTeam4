using MediatR;
using WorkoutService.Common.Responses;

namespace WorkoutService.Features.Workouts.Commands.CreateWorkout
{
  public record CreateWorkoutCommand(
        Guid WorkoutPlanId,
        string Name,
        string Category,
        string Difficulty,
        int DurationInMinutes,
        int CaloriesBurn,
        string ImageUrl,
        bool IsPremium,
        int DayNumber) : IRequest<RequestResult<Guid>>;
}
