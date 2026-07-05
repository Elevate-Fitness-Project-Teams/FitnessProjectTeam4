using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Common.Behaviors;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.References;
using WorkoutService.Features.Exercises.Dtos;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Exercises.Queries.GetPaginatedExercises
{
    public class GetPaginatedExercisesQueryHandler(
        IGenericRepository<Exercise> _repository,
        IMapper _mapper
        ) : IRequestHandler<GetPaginatedExercisesQuery, RequestResult<PaginatedList<ExerciseDto>>>
    {
        public async Task<RequestResult<PaginatedList<ExerciseDto>>> Handle(GetPaginatedExercisesQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.BodyPart))
                query = query.Where(e => e.TargetMuscles.Contains(request.BodyPart));

            if (!string.IsNullOrWhiteSpace(request.BodyPart))
                query = query.Where(e => e.EquipmentNeeded == request.Equipment);

            if (!string.IsNullOrWhiteSpace(request.SearchText))
                query = query.Where(e => e.Name.Contains(request.SearchText));

            int totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(e => e.Name)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ExerciseDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var pagedList = new PaginatedList<ExerciseDto>(items, totalCount, request.Page, request.PageSize);
            return RequestResult<PaginatedList<ExerciseDto>>.Success(pagedList);
        }
    }
}
