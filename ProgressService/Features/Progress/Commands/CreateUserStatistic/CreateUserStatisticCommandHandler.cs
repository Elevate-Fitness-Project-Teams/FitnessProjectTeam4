using MediatR;
using ProgressService.Domian.Aggregates.UserStatistics;
using ProgressService.Infrastructure.Data.Repositories;

namespace ProgressService.Features.Progress.Commands.CreateUserStatistic
{
    public class CreateUserStatisticCommandHandler(IGenericRepository<UserStatistic> _repository) : IRequestHandler<CreateUserStatisticCommand>
    {
        public async Task Handle(CreateUserStatisticCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(request.Statistic, cancellationToken);
        }
    }
}
