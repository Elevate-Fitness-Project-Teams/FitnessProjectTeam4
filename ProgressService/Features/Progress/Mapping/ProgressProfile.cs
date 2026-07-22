using AutoMapper;
using ProgressService.Domian.Aggregates.UserStatistics;
using ProgressService.Domian.Aggregates.WeightHistories;
using ProgressService.Domian.Aggregates.WorkoutLogs;
using ProgressService.Features.Progress.Dtos;
using ProgressService.Features.Progress.ViewModels;

namespace ProgressService.Features.Progress.Mapping
{
    public class ProgressProfile : Profile
    {
        public ProgressProfile()
        {
            CreateMap<LogWorkoutProgressResponseDto, LogProgressApiResponse>();

            CreateMap<LogWeightResponseDto, LogWeightApiResponse>();

            CreateMap<WeightHistory, WeightHistoryDto>();

            CreateMap<UserStatistic, UserStatsDto>();

            CreateMap<WeightHistoryDto, WeightHistoryApiResponse>();

            CreateMap<UserStatsDto, UserStatsApiResponse>();

            CreateMap<WorkoutLog, ProgressOverviewDto>()
                .ForMember(dest => dest.LogId, opt => opt.MapFrom(src => src.Id));

            CreateMap<ProgressOverviewDto, ProgressOverviewApiResponse>();

            CreateMap<CombinedProgressProfileDto, CombinedProgressProfileApiResponse>();
            CreateMap<RecentWorkoutLogDto, RecentWorkoutLogApiResponse>();
        }
    }
}
