using MediatR;

namespace ProgressService.Features.Progress.Common.Queries.GetWorkoutSessionById
{
    public record GetWorkoutSessionByIdQuery(Guid sessionId) : IRequest<bool>;

}
