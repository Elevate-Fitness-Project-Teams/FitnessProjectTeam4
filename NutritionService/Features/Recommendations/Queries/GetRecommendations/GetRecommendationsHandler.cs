using MediatR;
using Microsoft.AspNetCore.Http; 
using Microsoft.EntityFrameworkCore;
using NutritionService.Common;
using NutritionService.Common.Clients;
using NutritionService.Domain.Entities;
using NutritionService.Features.Recommendations.DTOs;
using NutritionService.Persistence.Repositories;

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace NutritionService.Features.Recommendations.Queries.GetRecommendations
{
    public class GetRecommendationsHandler
        : IRequestHandler<GetRecommendationsQuery, Result<RecommendationResponseDto>>
    {
        private readonly IRepository<Meal> _mealRepository;
        private readonly FceClient _fceClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetRecommendationsHandler(
            IRepository<Meal> mealRepository,
            FceClient fceClient,
            IHttpContextAccessor httpContextAccessor)
        {
            _mealRepository = mealRepository;
            _fceClient = fceClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<RecommendationResponseDto>> Handle(
            GetRecommendationsQuery request,
            CancellationToken cancellationToken)
        {
           
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                
                return Result<RecommendationResponseDto>.Failure(NutritionErrors.RequiredField, statusCode: 401);
            }
            var userId = Guid.Parse(userIdClaim);

            
            var fceMetrics = await _fceClient.GetUserMetricsAsync(userId, cancellationToken);

            if (fceMetrics == null || !fceMetrics.IsCalculated)
            {
                return Result<RecommendationResponseDto>.Failure(
                    NutritionErrors.FceMetricsNotCalculated,
                    statusCode: 400);
            }

            int userDailyGoalCalories = fceMetrics.CalorieTarget;

            
            var query = _mealRepository.Query();

            
            if (!string.IsNullOrEmpty(request.MealType) && Enum.TryParse<NutritionService.Domain.Enums.MealType>(request.MealType, true, out var mealTypeEnum))
            {
                query = query.Where(m => m.Type == mealTypeEnum);
            }

          
            if (request.MaxCalories.HasValue)
            {
                query = query.Where(m => m.NutritionFacts != null && m.NutritionFacts.Calories <= request.MaxCalories.Value);
            }

           
            if (request.MinProtein.HasValue)
            {
                query = query.Where(m => m.NutritionFacts != null && m.NutritionFacts.Protein >= request.MinProtein.Value);
            }

           
            var mealsQuery = query.Select(m => new RecommendedMealDto
            {
                MealId = m.MealId,
                Name = m.Name,
                MealType = m.Type.ToString(),
                Calories = m.NutritionFacts != null ? m.NutritionFacts.Calories : 0,
                ImageUrl = m.ImageUrl ?? string.Empty,
                Protein = m.NutritionFacts != null ? m.NutritionFacts.Protein : 0f
            });

            var pagedMeals = await mealsQuery.ToPagedResultAsync(request.Page, request.PageSize, cancellationToken);

           
            var responseDto = new RecommendationResponseDto
            {
                UserDailyGoalCalories = userDailyGoalCalories,
                RecommendedMeals = pagedMeals.Items.ToList()
            };

            return Result<RecommendationResponseDto>.Success(responseDto);
        }
    }
}