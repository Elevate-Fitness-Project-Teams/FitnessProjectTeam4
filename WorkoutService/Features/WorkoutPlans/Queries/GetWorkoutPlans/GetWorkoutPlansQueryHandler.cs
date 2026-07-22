using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Common.Behaviors;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Features.WorkoutPlans.Dtos;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.WorkoutPlans.Queries.GetWorkoutPlans
{
    public class GetWorkoutPlansQueryHandler(
        IGenericRepository<WorkoutPlan> _repository,
        IMapper _mapper)
        : IRequestHandler<GetWorkoutPlansQuery, RequestResult<PaginatedList<WorkoutPlanDto>>>
    {
        public async Task<RequestResult<PaginatedList<WorkoutPlanDto>>> Handle(GetWorkoutPlansQuery request, CancellationToken ct)
        {
            var query = _repository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.UserTier))
                query = query.Where(wp => wp.UserTier == request.UserTier);

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(wp => wp.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<WorkoutPlanDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            var paginatedResult = new PaginatedList<WorkoutPlanDto>(items, totalCount, request.PageNumber, request.PageSize);

            return RequestResult<PaginatedList<WorkoutPlanDto>>.Success(paginatedResult);
        }
    }
}
