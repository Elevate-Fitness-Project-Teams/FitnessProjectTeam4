using NutritionService.Features.Recommendations.DTOs;
using System.Net;

namespace NutritionService.Common.Clients
{
    public class FceClient
    {
        private readonly HttpClient _httpClient;
        public FceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<FceUserMetricsResponse?> GetUserMetricsAsync(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
               
                var response = await _httpClient.GetAsync($"api/v1/metrics/user-target?userId={userId}", cancellationToken);

                
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        return new FceUserMetricsResponse { IsCalculated = false };

                    return null;
                }

               
                var result = await response.Content.ReadFromJsonAsync<FceUserMetricsResponse>(cancellationToken: cancellationToken);
                return result;
            }
            catch (Exception)
            {
                
                return null;
            }
        }
    }
}
