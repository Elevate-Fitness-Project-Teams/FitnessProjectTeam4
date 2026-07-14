using MediatR;
using ProgressService.Domian.Aggregates.WorkoutLogs;
using ProgressService.Infrastructure.Data.Repositories;

namespace ProgressService.Features.Progress.Common.Queries.CheckSessionAlreadyLogged
{
    public class CheckSessionAlreadyLoggedQueryHandler(IGenericRepository<WorkoutLog> _repository) : IRequestHandler<CheckSessionAlreadyLoggedQuery, bool>
    {
        public async Task<bool> Handle(CheckSessionAlreadyLoggedQuery request, CancellationToken cancellationToken)
        {
           bool exists = await _repository.AnyAsync(ws => ws.SessionId == request.sessionId, cancellationToken);
            return exists;
        }
    }
}
