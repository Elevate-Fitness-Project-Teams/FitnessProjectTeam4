using FitnessCalculationEngine.Common.Messaging.Events;
using FitnessCalculationEngine.Features.Recalculation.Commands.Recalculate;
using MassTransit;
using MediatR;

namespace FitnessCalculationEngine.Features.Recalculation.Consumers;

public class WeightUpdatedConsumer(IMediator mediator) : IConsumer<WeightUpdatedEvent>
{
    public Task Consume(ConsumeContext<WeightUpdatedEvent> context) =>
        mediator.Send(new RecalculateCommand(
            context.Message.UserId,
            Reason: context.Message.Reason,
            NewWeight: context.Message.NewWeight,
            TriggeredBy: "weight_updated"), context.CancellationToken);
}
