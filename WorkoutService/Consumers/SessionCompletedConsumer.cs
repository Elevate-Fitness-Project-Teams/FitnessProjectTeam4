using MassTransit;
using MediatR;
using SharedMessages.Messages;
using WorkoutService.Features.Workouts.Commands.CompleteWorkoutSession;

namespace WorkoutService.Consumers
{
    public class SessionCompletedConsumer(
        IMediator _mediator,
        ILogger<SessionCompletedConsumer> _logger
        ) : IConsumer<WorkoutProgressLoggedMessage>
    {
        public async Task Consume(ConsumeContext<WorkoutProgressLoggedMessage> context)
        {
            _logger.LogInformation("Consumer Started");
           
            await _mediator.Send(new CompleteWorkoutSessionCommand(context.Message.SessionId), context.CancellationToken);
           
            _logger.LogInformation("Consumer Ended");
        }
    }
}
