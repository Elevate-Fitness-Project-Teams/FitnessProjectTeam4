using MediatR;
using NutritionService.Common;
using NutritionService.Common.Services;
using NutritionService.Domain.Entities;
using NutritionService.Domain.Enums;
using NutritionService.Features.Recommendations.DTOs;
using NutritionService.Persistence.Repositories;

namespace NutritionService.Features.Recommendations.Queries.GetRecommendationsByUserId
{
    public class GetRecommendationsByUserIdHandler
        : IRequestHandler<GetRecommendationsByUserIdQuery, ApiResponse<RecommendationResponseDto>>
    {
        private readonly IRepository<Meal> _mealRepository;
        private readonly IFceServiceClient _fceClient;

        public GetRecommendationsByUserIdHandler(
            IRepository<Meal> mealRepository,
            IFceServiceClient fceClient)
        {
            _mealRepository = mealRepository;
            _fceClient = fceClient;
        }

        public async Task<ApiResponse<RecommendationResponseDto>> Handle(
            GetRecommendationsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
           
            var calorieTarget = await _fceClient.GetCalorieTargetAsync(request.UserId);

           
            if (calorieTarget is null)
            {
                return ApiResponse<RecommendationResponseDto>.Fail("FCE_METRICS_NOT_CALCULATED", 400);
            }

          
            var query = _mealRepository.Query().AsQueryable();

            if (!string.IsNullOrEmpty(request.MealType)
                && Enum.TryParse<MealType>(request.MealType, true, out var mealType))
            {
                query = query.Where(m => m.Type == mealType);
            }

           
            var effectiveMaxCalories = request.MaxCalories ?? calorieTarget.Value;
            query = query.Where(m => m.NutritionFacts.Calories <= effectiveMaxCalories);

            if (request.MinProtein.HasValue)
            {
                query = query.Where(m => m.NutritionFacts.Protein >= request.MinProtein.Value);
            }

            
            var projectedQuery = query.Select(m => new RecommendedMealDto
            {
                MealId = m.MealId,
                Name = m.Name,
                Type = m.Type.ToString(),
                Calories = m.NutritionFacts.Calories,
                Protein = m.NutritionFacts.Protein,
                Carbs = m.NutritionFacts.Carbs,
                Fats = m.NutritionFacts.Fats,
                ImageUrl = m.ImageUrl
            });

            var pagedResult = await projectedQuery.ToPagedResultAsync(request.PageIndex, request.PageSize);

           
            var response = new RecommendationResponseDto
            {
                UserDailyGoalCalories = calorieTarget.Value,
                RecommendedMeals = pagedResult
            };

            return ApiResponse<RecommendationResponseDto>.Success(response);
        }
    }
}
