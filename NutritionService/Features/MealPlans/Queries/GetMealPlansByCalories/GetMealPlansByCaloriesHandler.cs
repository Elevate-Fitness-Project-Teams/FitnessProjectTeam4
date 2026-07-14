using MediatR;
using Microsoft.EntityFrameworkCore;
using NutritionService.Common;
using NutritionService.Domain.Entities;
using NutritionService.Features.MealPlans.DTOs;
using NutritionService.Persistence.Repositories;

namespace NutritionService.Features.MealPlans.Queries.GetMealPlansByCalories
{
    public class GetMealPlansByCaloriesHandler
        : IRequestHandler<GetMealPlansByCaloriesQuery, Result<PagedResult<MealPlanByCaloriesDto>>>
    {
        private readonly IRepository<MealPlan> _mealPlanRepository;

        public GetMealPlansByCaloriesHandler(IRepository<MealPlan> mealPlanRepository)
        {
            _mealPlanRepository = mealPlanRepository;
        }

        public async Task<Result<PagedResult<MealPlanByCaloriesDto>>> Handle(
            GetMealPlansByCaloriesQuery request,
            CancellationToken cancellationToken)
        {
           
            if (request.Calories is null)
            {
                return Result<PagedResult<MealPlanByCaloriesDto>>.Failure(
                    NutritionErrors.RequiredField,
                    statusCode: 400);
            }

           
            var query = _mealPlanRepository.Query()
                .Where(mp => mp.TargetCalorieRangeMin <= request.Calories &&
                             mp.TargetCalorieRangeMax >= request.Calories)
                .OrderByDescending(mp => mp.CreatedAt) 
                .Select(mp => new MealPlanByCaloriesDto
                {
                    MealPlanId = mp.MealPlanId,
                    Name = mp.Name,
                    Description = mp.Description,
                    TargetCalorieRangeMin = mp.TargetCalorieRangeMin,
                    TargetCalorieRangeMax = mp.TargetCalorieRangeMax
                });

           
            var pagedResult = await query.ToPagedResultAsync(
                request.PageIndex,
                request.PageSize,
                cancellationToken);

          
            return Result<PagedResult<MealPlanByCaloriesDto>>.Success(pagedResult);
        }
    }
}
