using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Features.WorkoutPlans.Dtos;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.WorkoutPlans.Queries.GetWorkoutPlanById
{
    public class GetWorkoutPlanByIdQueryHandler(
        IGenericRepository<WorkoutPlan> _repository,
        IMapper _mapper) : IRequestHandler<GetWorkoutPlanByIdQuery, RequestResult<WorkoutPlanDto>>
    {
        public async Task<RequestResult<WorkoutPlanDto>> Handle(GetWorkoutPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var planDto = await _repository.GetAll()
                .Where(p => p.Id == request.Id)
                .ProjectTo<WorkoutPlanDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (planDto == null)
                return RequestResult<WorkoutPlanDto>.Failure(ErrorCode.WorkoutPlanNotFound,$"Workout Plan with ID '{request.Id}' was not found.");

            return RequestResult<WorkoutPlanDto>.Success(planDto);
        }
    }
}
