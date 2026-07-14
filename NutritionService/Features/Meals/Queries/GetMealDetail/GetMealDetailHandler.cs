using MediatR;
using Microsoft.EntityFrameworkCore;
using NutritionService.Common;
using NutritionService.Domain.Entities;
using NutritionService.Features.Meals.DTOs;
using NutritionService.Persistence.Repositories;

namespace NutritionService.Features.Meals.Queries.GetMealDetail
{
    public class GetMealDetailHandler : IRequestHandler<GetMealDetailQuery, Result<MealDetailDto>>
    {
        private readonly IRepository<Meal> _mealRepository;

        public GetMealDetailHandler(IRepository<Meal> mealRepository)
        {
            _mealRepository = mealRepository;
        }
        public async Task<Result<MealDetailDto>> Handle(GetMealDetailQuery request, CancellationToken cancellationToken)
        {
            var meal = await _mealRepository.Query()
                .Where(m => m.MealId == request.MealId)
                .Select(m => new MealDetailDto
                {
                    MealId = m.MealId,
                    Name = m.Name,
                    Type = m.Type.ToString(),
                    PrepTimeInMinutes = m.PrepTimeInMinutes,
                    Difficulty = m.Difficulty.ToString(),
                    ImageUrl = m.ImageUrl,
                    Calories = m.NutritionFacts != null ? m.NutritionFacts.Calories : 0,
                    Protein = m.NutritionFacts != null ? m.NutritionFacts.Protein : 0,
                    Carbs = m.NutritionFacts != null ? m.NutritionFacts.Carbs : 0,
                    Fats = m.NutritionFacts != null ? m.NutritionFacts.Fats : 0,
                    Fiber = m.NutritionFacts != null ? m.NutritionFacts.Fiber : 0,
                    Ingredients = m.IngredientsJson,
                    Instructions = m.InstructionsJson,
                    Allergens = m.AllergensJson,
                    Tags = m.TagsJson,
                    Variations = m.VariationsJson
                })
                .FirstOrDefaultAsync(cancellationToken);

           
            if (meal == null)
            {
                return Result<MealDetailDto>.Failure(NutritionErrors.MealNotFound, 404);
            }

            
            return Result<MealDetailDto>.Success(meal);
        }
    }
}
