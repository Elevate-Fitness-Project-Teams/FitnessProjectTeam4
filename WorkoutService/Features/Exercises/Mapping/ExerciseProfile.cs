using AutoMapper;
using WorkoutService.Domain.References;
using WorkoutService.Features.Exercises.Dtos;
using WorkoutService.Features.Exercises.ViewModels;

namespace WorkoutService.Features.Exercises.Mapping
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile() 
        {
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseDto, ExerciseCardViewModel>();

            CreateMap<Exercise, ExerciseDetailsDto>();
            CreateMap<ExerciseDetailsDto, ExerciseDetailsViewModel>();
        }
    }
}
