using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Features.Workouts.Dtos;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Workouts.Queries.GetWorkoutsByCategory
{
    public class GetWorkoutsByCategoryQueryHandler(
        IGenericRepository<Workout> _repository,
        IMapper _mapper) :
        IRequestHandler<GetWorkoutsByCategoryQuery, RequestResult<List<WorkoutDto>>>
    {
        public async Task<RequestResult<List<WorkoutDto>>> Handle(GetWorkoutsByCategoryQuery request, CancellationToken cancellationToken)
        {
           
            var workouts = await _repository.GetAll()
                .Where(w => w.Category == request.CategoryName)
                .OrderBy(w => w.Name)
                .ProjectTo<WorkoutDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (workouts == null || workouts.Count == 0)
                return RequestResult<List<WorkoutDto>>.Failure(ErrorCode.WorkoutNotFound, $"No workouts found for category '{request.CategoryName}'.");

            return RequestResult<List<WorkoutDto>>.Success(workouts);
        }
    }
}
