using MediatR;
using Microsoft.AspNetCore.Http;
using NutritionService.Common;
using NutritionService.Common.Clients;
using NutritionService.Domain.Entities;
using NutritionService.Features.Recommendations.DTOs;
using NutritionService.Features.Recommendations.Extensions;
using NutritionService.Persistence.Repositories;
using System.Security.Claims;

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
            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Result<RecommendationResponseDto>.Failure(
                    NutritionErrors.UnauthorizedUser, statusCode: 401);
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

            var mealsQuery = _mealRepository.Query()
                .ApplyRecommendationsFilter(request.MealType, request.MaxCalories, request.MinProtein);

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