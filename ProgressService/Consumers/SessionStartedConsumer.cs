using MassTransit;
using ProgressService.Domian.References;
using ProgressService.Infrastructure.Data.Repositories;
using SharedMessages.Messages;

namespace ProgressService.Consumers
{
    public class SessionStartedConsumer(
        IGenericRepository<WorkoutSessionReference> _repository,
        IUnitOfWork _unitOfWork
        ) : IConsumer<SessionStartedMessage>
    {
        public async Task Consume(ConsumeContext<SessionStartedMessage> context)
        {
            await _unitOfWork.ExecuteAsync(async () =>
            {
               var command = context.Message;
               var refernces = new WorkoutSessionReference()
               {
                   SessionId = command.SessionId,
                   WorkoutId = command.WorkoutId,
                   UserId = command.UserId,
                   StartTime = command.StartTime,
                   Status = command.Status
               };

               await _repository.AddAsync(refernces, context.CancellationToken);

               return true;
            }, context.CancellationToken);

        }
    }
}
