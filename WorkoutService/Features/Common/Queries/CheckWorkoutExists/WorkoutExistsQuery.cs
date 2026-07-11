using MediatR;

namespace WorkoutService.Features.Common.Queries.CheckWorkoutExists
{
    public record WorkoutExistsQuery(Guid WorkoutId) : IRequest<bool>;
}
