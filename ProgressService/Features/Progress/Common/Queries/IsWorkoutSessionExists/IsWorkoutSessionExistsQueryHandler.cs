using MediatR;
using ProgressService.Domian.References;
using ProgressService.Infrastructure.Data.Repositories;

namespace ProgressService.Features.Progress.Common.Queries.IsWorkoutSessionExists
{
    public class IsWorkoutSessionExistsQueryHandler(
        IGenericRepository<WorkoutSessionReference> _repository
        ) : IRequestHandler<IsWorkoutSessionExistsQuery, bool>
    {
        public async Task<bool> Handle(IsWorkoutSessionExistsQuery request, CancellationToken cancellationToken)
        {
            bool workoutSessionExists = await _repository.AnyAsync(x => x.SessionId == request.SessionId && x.UserId == request.UserId, cancellationToken);
           return workoutSessionExists;
        }
    }
}
