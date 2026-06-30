using MediatR;
using Microsoft.EntityFrameworkCore;
using NutritionService.Common;
using NutritionService.Domain.Entities;
using NutritionService.Features.MealPlans.DTOs;
using NutritionService.Persistence.Repositories;

namespace NutritionService.Features.MealPlans.Queries.GetMealPlansByCalories
{
    public class GetMealPlansByCaloriesHandler 
        : IRequestHandler<GetMealPlansByCaloriesQuery, ApiResponse<List<MealPlanByCaloriesDto>>>
    {
        private readonly IRepository<MealPlan> _mealPlanRepository;

        public GetMealPlansByCaloriesHandler(IRepository<MealPlan> mealPlanRepository)
        {
            _mealPlanRepository = mealPlanRepository;
        }

        public async Task<ApiResponse<List<MealPlanByCaloriesDto>>> Handle(
            GetMealPlansByCaloriesQuery request, 
            CancellationToken cancellationToken)
        {
            var mealPlans = await _mealPlanRepository.Query()
                .Where(mp => mp.TargetCalorieRangeMin <= request.Calories 
                          && mp.TargetCalorieRangeMax >= request.Calories)
                .Select(mp => new MealPlanByCaloriesDto
                {
                    MealPlanId = mp.MealPlanId,
                    Name = mp.Name,
                    Description = mp.Description,
                    TargetCalorieRangeMin = mp.TargetCalorieRangeMin,
                    TargetCalorieRangeMax = mp.TargetCalorieRangeMax,
                    Items = mp.MealPlanItems.Select(mpi => new MealPlanItemDto
                    {
                        Id = mpi.Id,
                        MealId = mpi.MealId,
                        MealName = mpi.Meal.Name,
                        DayOfWeek = mpi.DayOfWeek.ToString(),
                        MealTime = mpi.MealTime.ToString()
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            if (mealPlans.Count == 0)
            {
                return ApiResponse<List<MealPlanByCaloriesDto>>.Fail("RES_NO_PLAN_MATCHES_CALORIES", 404);
            }

            return ApiResponse<List<MealPlanByCaloriesDto>>.Success(mealPlans);
        }
    }
}
