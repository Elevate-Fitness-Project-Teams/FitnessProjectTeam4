using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Common.GenericResult;
using ProfileService.Domain.Entities;
using ProfileService.Features.ViewProfile.DTOs;
using ProfileService.Infrastructure.Repository;

namespace ProfileService.Features.ViewProfile.Queries.Handler
{
    public class GetProfileStatisticsHandler : IRequestHandler<GetProfileStatisticsQuery, RequestResult<ProfileStatisticsDto>>
    {
        private readonly IGenericRepository<UserStatisticCache> _statisticsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProfileStatisticsHandler> _logger;

        public GetProfileStatisticsHandler(
            IGenericRepository<UserStatisticCache> statisticsRepository,
            IMapper mapper,
            ILogger<GetProfileStatisticsHandler> logger)
        {
            _statisticsRepository = statisticsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RequestResult<ProfileStatisticsDto>> Handle(GetProfileStatisticsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching statistics cache for user ID: {UserId}", request.UserId);

            var statistics = await _statisticsRepository
                .GetAsync(s => s.Id == request.UserId, asNoTracking: true)
                .FirstOrDefaultAsync(cancellationToken);

            if (statistics == null)
            {
                _logger.LogInformation("No statistics cache found for user ID: {UserId}. Returning default zeros.", request.UserId);

                var defaultDto = new ProfileStatisticsDto { UserId = request.UserId, TotalWorkouts = 0, CurrentStreak = 0 };
                // نرجع Success مع الـ DTO الافتراضي
                return RequestResult<ProfileStatisticsDto>.Success(defaultDto);
            }

            var dto = _mapper.Map<ProfileStatisticsDto>(statistics);
            return RequestResult<ProfileStatisticsDto>.Success(dto);
        }
    }
}
