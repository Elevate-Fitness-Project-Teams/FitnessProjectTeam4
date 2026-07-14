
namespace SharedMessages.Messages
{
    public record WeightUpdatedIntegrationMessage(
         string UserId,
         double NewWeight,
         DateTime RecordedAt
    );
}
