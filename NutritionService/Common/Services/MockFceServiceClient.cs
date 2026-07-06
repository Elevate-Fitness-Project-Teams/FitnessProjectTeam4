namespace NutritionService.Common.Services
{
    public class MockFceServiceClient : IFceServiceClient
    {
       
        private static readonly Dictionary<Guid, int> _calculatedUsers = new()
        {
           
            { Guid.Parse("11111111-1111-1111-1111-111111111111"), 2200 },
            { Guid.Parse("22222222-2222-2222-2222-222222222222"), 1800 }
        };

        public Task<int?> GetCalorieTargetAsync(Guid userId)
        {
            
            if (_calculatedUsers.TryGetValue(userId, out var calories))
            {
                return Task.FromResult<int?>(calories);
            }

            return Task.FromResult<int?>(null);
        }
    }
}
