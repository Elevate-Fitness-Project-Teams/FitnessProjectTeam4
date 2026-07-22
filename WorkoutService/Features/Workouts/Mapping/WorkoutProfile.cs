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
            CreateMap<WorkoutDto, WorkoutViewModel>();

            CreateMap<WorkoutExercise, WorkoutExerciseDto>();

            CreateMap<Workout, WorkoutDetailsDto>();
            CreateMap<WorkoutDetailsDto, WorkoutDetailsViewModel>();
        }
    }
}
