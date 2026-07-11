using MassTransit;
using MediatR;
using SharedMessages.Messages;
using WorkoutService.Features.Workouts.Commands.CompleteWorkoutSession;

namespace WorkoutService.Consumers
{
    public class SessionCompletedConsumer(IMediator _mediator) : IConsumer<WorkoutProgressLoggedMessage>
    {
        public async Task Consume(ConsumeContext<WorkoutProgressLoggedMessage> context)
        {
           await _mediator.Send(new CompleteWorkoutSessionCommand(context.Message.SessionId), context.CancellationToken);
        }
    }
}
