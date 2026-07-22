using MediatR;

namespace WorkoutService.Features.Common.Queries.CheckExerciseExists
{
    public record ExerciseExistQuery(Guid Id) : IRequest<bool>;
}
