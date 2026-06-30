using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Features.Workouts.Dtos;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Workouts.Queries.GetWorkoutById
{
    public class GetWorkoutByIdQueryHandler(IGenericRepository<Workout> _workoutRepository, IMapper _mapper) : 
        IRequestHandler<GetWorkoutByIdQuery, RequestResult<WorkoutDetailsDto>>
    {
        public async Task<RequestResult<WorkoutDetailsDto>> Handle(GetWorkoutByIdQuery request, CancellationToken cancellationToken)
        {
            var workoutDto = await _workoutRepository.GetAll()
                .Where(w => w.Id == request.Id)
                .ProjectTo<WorkoutDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (workoutDto == null)
                return RequestResult<WorkoutDetailsDto>.Failure(ErrorCode.WorkoutNotFound,$"Workout with ID '{request.Id}' was not found.");

            var sortedExercises = workoutDto.WorkoutExercises.OrderBy(e => e.OrderIndex).ToList();
            workoutDto = workoutDto with { WorkoutExercises = sortedExercises };

            return RequestResult<WorkoutDetailsDto>.Success(workoutDto);
        }
    }
}
