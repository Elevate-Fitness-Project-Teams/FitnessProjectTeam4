using MediatR;
using WorkoutService.Common.Behaviors;
using WorkoutService.Common.Responses;
using WorkoutService.Features.Exercises.Dtos;

namespace WorkoutService.Features.Exercises.Queries.GetPaginatedExercises
{
    public record GetPaginatedExercisesQuery(
        string? BodyPart,
        string? Equipment,
        string? SearchText,
        int Page,
        int PageSize) : IRequest<RequestResult<PaginatedList<ExerciseDto>>>;
}
