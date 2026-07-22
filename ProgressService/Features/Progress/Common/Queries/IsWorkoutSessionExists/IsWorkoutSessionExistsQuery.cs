using MediatR;

namespace ProgressService.Features.Progress.Common.Queries.IsWorkoutSessionExists
{
    public record IsWorkoutSessionExistsQuery( Guid SessionId, Guid UserId) : IRequest<bool>;
}
