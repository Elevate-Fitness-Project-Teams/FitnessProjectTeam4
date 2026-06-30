using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Features.Exercises.Dtos;

namespace WorkoutService.Features.Exercises.Queries.GetExerciseById
{
    public record GetExerciseByIdQuery(Guid Id) : IRequest<RequestResult<ExerciseDetailsDto>>;
}
