using MassTransit;
using NutritionSharedMessages.Messages;

namespace NutritionService.Common.Clients
{
    public class FceClient
    {
        private readonly IRequestClient<GetUserCalorieTargetRequest> _requestClient;

        public FceClient(IRequestClient<GetUserCalorieTargetRequest> requestClient)
        {
            _requestClient = requestClient;
        }

        public async Task<GetUserCalorieTargetResponse?> GetUserMetricsAsync(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _requestClient.GetResponse<GetUserCalorieTargetResponse>(
                    new GetUserCalorieTargetRequest { UserId = userId },
                    cancellationToken);

                return response.Message;
            }
            catch (RequestTimeoutException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
