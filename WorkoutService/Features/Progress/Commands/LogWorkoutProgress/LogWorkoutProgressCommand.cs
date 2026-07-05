using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Features.Progress.Dtos;

namespace WorkoutService.Features.Progress.Commands.LogWorkoutProgress
{
    public record LogWorkoutProgressCommand(
        Guid WorkoutId,
        Guid SessionId,
        string UserId, 
        DateTime CompletedAt,
        int Duration,
        int CaloriesBurned,
        string Difficulty,
        string Notes,
        int Rating,
        List<ExerciseCompletedDto> ExercisesCompleted
    ) : IRequest<RequestResult<LogWorkoutProgressResponseDto>>;
}
