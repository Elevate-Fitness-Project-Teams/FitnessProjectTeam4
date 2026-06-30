using AutoMapper;
using WorkoutService.Features.Progress.Dtos;
using WorkoutService.Features.Progress.ViewModels;

namespace WorkoutService.Features.Progress.Mapping
{
    public class WorkoutProgressProfile : Profile
    {
        public WorkoutProgressProfile()
        {
            CreateMap<LogWorkoutProgressResponseDto, LogProgressApiResponseViewModel>();
        }
    }
}
