using MediatR;
using NutritionService.Common;
using NutritionService.Common.Clients;
using NutritionService.Domain.Entities;
using NutritionService.Features.Recommendations.DTOs;
using NutritionService.Features.Recommendations.Extensions;
using NutritionService.Persistence.Repositories;

namespace NutritionService.Features.Recommendations.Queries.GetRecommendationsByUserId
{
    public class GetRecommendationsByUserIdHandler
        : IRequestHandler<GetRecommendationsByUserIdQuery, Result<RecommendationResponseDto>>
    {
        private readonly IRepository<Meal> _mealRepository;
        private readonly FceClient _fceClient;

        public GetRecommendationsByUserIdHandler(
            IRepository<Meal> mealRepository,
            FceClient fceClient)
        {
            _mealRepository = mealRepository;
            _fceClient = fceClient;
        }

        public async Task<Result<RecommendationResponseDto>> Handle(
            GetRecommendationsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var fceMetrics = await _fceClient.GetUserMetricsAsync(request.UserId, cancellationToken);

            if (fceMetrics == null || !fceMetrics.IsCalculated)
            {
                return Result<RecommendationResponseDto>.Failure(
                    NutritionErrors.FceMetricsNotCalculated,
                    statusCode: 400);
            }

            int userDailyGoalCalories = fceMetrics.CalorieTarget;

            var mealsQuery = _mealRepository.Query()
                .ApplyRecommendationsFilter(request.MealType, request.MaxCalories, request.MinProtein);

            var pagedMeals = await mealsQuery.ToPagedResultAsync(request.PageIndex, request.PageSize, cancellationToken);

            var responseDto = new RecommendationResponseDto
            {
                UserDailyGoalCalories = userDailyGoalCalories,
                RecommendedMeals = pagedMeals.Items.ToList()
            };

            return Result<RecommendationResponseDto>.Success(responseDto);
        }
    }
}
