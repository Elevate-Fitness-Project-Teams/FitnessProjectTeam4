using MediatR;
using ProgressService.Domian.References;
using ProgressService.Infrastructure.Data.Repositories;

namespace ProgressService.Features.Progress.Common.Queries.GetWorkoutSessionById
{
    public class GetWorkoutSessionByIdQueryHandler(IGenericRepository<WorkoutSessionReference> _repository) : IRequestHandler<GetWorkoutSessionByIdQuery, bool>
    {
        public async Task<bool> Handle(GetWorkoutSessionByIdQuery request, CancellationToken cancellationToken)
        {
            bool exists = await _repository.AnyAsync(ws => ws.SessionId == request.sessionId, cancellationToken);
            return exists;
        }
    }
}
