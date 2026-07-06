using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.References;
using WorkoutService.Features.Exercises.Dtos;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Exercises.Queries.GetExerciseById
{
    public class GetExerciseByIdQueryHandler(
        IGenericRepository<Exercise> _repository,
        IMapper _mapper ) : IRequestHandler<GetExerciseByIdQuery, RequestResult<ExerciseDetailsDto>>
    {
        public async Task<RequestResult<ExerciseDetailsDto>> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
        {
            var exerciseDto = await _repository.GetAll()
                .Where(e => e.Id == request.Id)
                .ProjectTo<ExerciseDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (exerciseDto == null)
                return RequestResult<ExerciseDetailsDto>.Failure(ErrorCode.ExerciseNotFound,$"Exercise with ID '{request.Id}' was not found in the catalog.");

            return RequestResult<ExerciseDetailsDto>.Success(exerciseDto);
        }
    }
}
