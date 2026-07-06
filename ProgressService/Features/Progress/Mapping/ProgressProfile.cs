using AutoMapper;
using ProgressService.Features.Progress.Commands.LogWorkoutProgress;
using ProgressService.Features.Progress.Dtos;
using ProgressService.Features.Progress.ViewModels;

namespace ProgressService.Features.Progress.Mapping
{
    public class ProgressProfile : Profile
    {
        public ProgressProfile()
        {
            CreateMap<LogProgressApiRequest, LogWorkoutProgressOrchestrator>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<ExerciseCompletedApiItem, ExerciseCompletedDto>();

            CreateMap<LogWorkoutProgressResponseDto, LogProgressApiResponse>();
        }
    }
}
