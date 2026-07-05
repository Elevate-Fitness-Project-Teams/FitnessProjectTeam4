using MediatR;
using NutritionService.Common;
using NutritionService.Domain.Entities;
using NutritionService.Features.MealPlans.DTOs;
using NutritionService.Persistence.Repositories;

namespace NutritionService.Features.MealPlans.Queries.BrowseMealPlans
{
    public class BrowseMealPlansHandler : IRequestHandler<BrowseMealPlansQuery, ApiResponse<PagedResult<BrowseMealPlanDto>>>
    {
        private readonly IRepository<MealPlan> _mealPlanRepository;
        public BrowseMealPlansHandler(IRepository<MealPlan> mealPlanRepository)
        {
            _mealPlanRepository = mealPlanRepository;
        }
        public async Task<ApiResponse<PagedResult<BrowseMealPlanDto>>> Handle(
            BrowseMealPlansQuery request,
            CancellationToken cancellationToken)
        {
            var query = _mealPlanRepository.Query()
                .Select(mp => new BrowseMealPlanDto
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
                });
           
            var pagedPlans = await query.ToPagedResultAsync(request.PageIndex, request.PageSize);
            
            return ApiResponse<PagedResult<BrowseMealPlanDto>>.Success(pagedPlans);
        }
    }
}
