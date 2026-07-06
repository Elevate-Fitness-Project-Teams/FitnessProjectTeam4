using MediatR;
using ProgressService.Common.Responses;
using ProgressService.Features.Progress.Dtos;

namespace ProgressService.Features.Progress.Commands.LogWorkoutProgress
{
    public record LogWorkoutProgressOrchestrator(
        Guid WorkoutId,
        Guid SessionId,
        string UserId,
        DateTime CompletedAt,
        int DurationInMinutes,
        int CaloriesBurned,
        string Difficulty,
        string? Notes,
        int Rating,
        List<ExerciseCompletedDto> ExercisesCompleted
    ) : IRequest<RequestResult<LogWorkoutProgressResponseDto>>;

    public record ExerciseCompletedDto(
        Guid ExerciseId,
        int SetsCompleted,
        int RepsCompleted,
        decimal WeightUsed,
        bool Completed
    );
}
