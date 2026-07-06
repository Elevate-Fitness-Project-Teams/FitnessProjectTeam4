using MediatR;
using ProgressService.Domian.Aggregates.UserStreaks;
using ProgressService.Infrastructure.Data.Repositories;

namespace ProgressService.Features.Progress.Commands.CreateUserStreak
{
    public class CreateUserStreakCommandHandler(IGenericRepository<UserStreak> _repository) : IRequestHandler<CreateUserStreakCommand>
    {
        public async Task Handle(CreateUserStreakCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(request.Streak ,cancellationToken);
        }
    }
}
