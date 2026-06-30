using AutoMapper;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Features.Workouts.Dtos;
using WorkoutService.Features.Workouts.ViewModels;

namespace WorkoutService.Features.Workouts.Mapping
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<Workout, WorkoutDto>();
            CreateMap<Workout, WorkoutViewModel>();

            CreateMap<WorkoutExercise, WorkoutExerciseDto>()
            .ForMember(dest => dest.ExerciseName, opt => opt.MapFrom(src => src.Exercise.Name));

            CreateMap<Workout, WorkoutDetailsDto>();
            CreateMap<WorkoutDetailsDto, WorkoutDetailsViewModel>();
        }
    }
}
