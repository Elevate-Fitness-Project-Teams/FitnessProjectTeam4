using MediatR;
using WorkoutService.Domain.References;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Common.Queries.CheckExerciseExists
{
    public class ExerciseExistQueryHandler(IGenericRepository<Exercise> _repository) : IRequestHandler<ExerciseExistQuery, bool>
    {
        public async Task<bool> Handle(ExerciseExistQuery request, CancellationToken cancellationToken)
        {
            return await _repository.AnyAsync(e => e.Id == request.Id, cancellationToken);
        }
    }
}
