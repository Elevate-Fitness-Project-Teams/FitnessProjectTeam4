using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Common.Behaviors;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Features.Workouts.Dtos;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Workouts.Queries.GetWorkouts
{
    public class GetWorkoutsQueryHandler(IGenericRepository<Workout> _workoutRepository, IMapper _mapper)
     : IRequestHandler<GetWorkoutsQuery, RequestResult<PaginatedList<WorkoutDto>>>
    {
        public async Task<RequestResult<PaginatedList<WorkoutDto>>> Handle(GetWorkoutsQuery request, CancellationToken ct)
        { 
            var query = _workoutRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.Category))
                query = query.Where(w => w.Category == request.Category);

            if (!string.IsNullOrWhiteSpace(request.Difficulty))
                query = query.Where(w => w.Difficulty == request.Difficulty);

            if (!string.IsNullOrWhiteSpace(request.SearchText))
                query = query.Where(w => w.Name.Contains(request.SearchText));

            int totalCount = await query.CountAsync(ct);

            var items = await query
                .OrderBy(w => w.Name)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<WorkoutDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            var pagedList = new PaginatedList<WorkoutDto>(items, totalCount, request.Page, request.PageSize);

            return RequestResult<PaginatedList<WorkoutDto>>.Success(pagedList);
        }
    }
}
