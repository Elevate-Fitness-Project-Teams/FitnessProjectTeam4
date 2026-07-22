using MediatR;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.References;
using WorkoutService.Infrastructure.Data.Repositories;

namespace WorkoutService.Features.Exercises.Commands.CreateExercise
{
    public class CreateExerciseCommandHandler(
        IGenericRepository<Exercise> _repository,
        IUnitOfWork _unitOfWork,
        ILogger<CreateExerciseCommandHandler> _logger
        ) : IRequestHandler<CreateExerciseCommand, RequestResult<Guid>>
    {
        public async Task<RequestResult<Guid>> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new exercise with name: {Name}", request.Name);
            try
            {
                _logger.LogInformation("Checking if exercise with name: {Name} already exists", request.Name);
                bool exerciseExists = await _repository.AnyAsync(e => e.Name == request.Name, cancellationToken);
                if (exerciseExists)
                {
                    _logger.LogWarning("Exercise with name: {Name} already exists", request.Name);
                    return RequestResult<Guid>.Failure(ErrorCode.ExerciseAlreadyExists, "Exercise with the same name already exists.");
                }

                return await _unitOfWork.ExecuteAsync(async () =>
                {
                    var newExercise = new Exercise(
                          Guid.NewGuid(),
                          request.Name,
                          request.Description,
                          request.Difficulty,
                          request.VideoUrl,
                          request.TargetMuscles,
                          request.EquipmentNeeded
                    );        

                    _logger.LogInformation("Adding new exercise with name: {Name} to the repository", request.Name);
                    await _repository.AddAsync(newExercise, cancellationToken);

                    return RequestResult<Guid>.Success(newExercise.Id);
                },cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new exercise with name: {Name}", request.Name);
                return RequestResult<Guid>.Failure(ErrorCode.InternalServerError, "An error occurred while creating the exercise.");
            }
        }
    }
}
