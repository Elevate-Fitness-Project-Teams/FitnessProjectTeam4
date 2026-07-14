using MediatR;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Common.Queries.CheckWorkoutExists
{
    public class WorkoutExistsQueryHandler(IGenericRepository<Workout> _repository) : IRequestHandler<WorkoutExistsQuery,bool>
    {
        public async Task<bool> Handle(WorkoutExistsQuery request, CancellationToken cancellationToken)
        {
           bool exists = await _repository.AnyAsync(w => w.Id == request.WorkoutId, cancellationToken);
            return exists;
        }
    }
}
