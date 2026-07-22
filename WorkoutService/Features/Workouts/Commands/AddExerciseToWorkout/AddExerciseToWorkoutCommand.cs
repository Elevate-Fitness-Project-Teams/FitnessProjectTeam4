using MediatR;
using WorkoutService.Common.Responses;

namespace WorkoutService.Features.Workouts.Commands.AddExerciseToWorkout
{
    public record AddExerciseToWorkoutCommand(
        Guid WorkoutId,
        Guid ExerciseId,
        int Sets,
        string Reps,
        int RestTimeInSeconds
    ) : IRequest<RequestResult<Unit>>;
}
