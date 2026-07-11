using MediatR;
using NutritionService.Common;
using NutritionService.Domain.Entities;
using NutritionService.Features.MealPlans.DTOs;
using NutritionService.Persistence.Repositories;

namespace NutritionService.Features.MealPlans.Queries.BrowseMealPlans
{
    public class BrowseMealPlansHandler : IRequestHandler<BrowseMealPlansQuery, Result<PagedResult<BrowseMealPlanDto>>>
    {
        private readonly IRepository<MealPlan> _mealPlanRepository;
        public BrowseMealPlansHandler(IRepository<MealPlan> mealPlanRepository)
        {
            _mealPlanRepository = mealPlanRepository;
        }
        public async Task<Result<PagedResult<BrowseMealPlanDto>>> Handle(BrowseMealPlansQuery request, CancellationToken cancellationToken)
        {
            var query = _mealPlanRepository.Query()
               
                .OrderByDescending(mp => mp.CreatedAt)
                .Select(mp => new BrowseMealPlanDto
                {
                    MealPlanId = mp.MealPlanId,
                    Name = mp.Name,
                    Description = mp.Description,
                    TargetCalorieRangeMin = mp.TargetCalorieRangeMin,
                    TargetCalorieRangeMax = mp.TargetCalorieRangeMax,

                    
                    Items = mp.MealPlanItems.Select(item => new MealPlanItemDto
                    {
                        Id = item.Id,
                        MealId = item.MealId,
                        MealName = item.Meal.Name,
                        DayOfWeek = item.DayOfWeek.ToString(),
                        MealTime = item.MealTime.ToString()
                    }).ToList()
                });

            var pagedResult = await query.ToPagedResultAsync(request.PageIndex, request.PageSize, cancellationToken);

            return Result<PagedResult<BrowseMealPlanDto>>.Success(pagedResult);
        }
    }
}
