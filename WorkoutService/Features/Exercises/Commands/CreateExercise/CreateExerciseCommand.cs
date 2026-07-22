using MediatR;
using WorkoutService.Common.Responses;

namespace WorkoutService.Features.Exercises.Commands.CreateExercise
{
    public record CreateExerciseCommand(
        string Name,
        List<string> TargetMuscles,
        string EquipmentNeeded,
        string Description,
        string VideoUrl,
        string Difficulty
    ) : IRequest<RequestResult<Guid>>;

}
