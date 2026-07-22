using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Features.Workouts.Dtos;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Workouts.Queries.GetWorkoutsByPlan
{
    public class GetWorkoutByPlanQueryHandler(
        IGenericRepository<Workout> _repository,
        IMapper _mapper) : 
        IRequestHandler<GetWorkoutByPlanQuery, RequestResult<List<WorkoutDto>>>
    {
        public async Task<RequestResult<List<WorkoutDto>>> Handle(GetWorkoutByPlanQuery request, CancellationToken cancellationToken)
        {
            var workouts = await _repository.GetAll()
               .Where(w => w.WorkoutPlanId == request.PlanId)
               .OrderBy(w => w.DayNumber)
               .ProjectTo<WorkoutDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);

            if (workouts is null || workouts.Count == 0)
                return RequestResult<List<WorkoutDto>>.Failure(ErrorCode.WorkoutNotFound, $"No workouts found for plan with ID {request.PlanId}");

            return RequestResult<List<WorkoutDto>>.Success(workouts);
        }
    }
}
