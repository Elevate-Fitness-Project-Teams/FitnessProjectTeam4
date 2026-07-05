namespace NutritionService.Common.Services
{
    public interface IFceServiceClient
    {
        Task<int?> GetCalorieTargetAsync(Guid userId);
    }
}
