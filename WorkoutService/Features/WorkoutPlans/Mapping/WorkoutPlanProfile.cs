using AutoMapper;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Features.WorkoutPlans.Commands;
using WorkoutService.Features.WorkoutPlans.Dtos;
using WorkoutService.Features.WorkoutPlans.ViewModels;

namespace WorkoutService.Features.WorkoutPlans.Mapping
{
    public class WorkoutPlanProfile : Profile
    {
        public WorkoutPlanProfile()
        {
            CreateMap<WorkoutPlan, WorkoutPlanDto>();
            CreateMap<WorkoutPlanDto, WorkoutPlanViewModel>();
        }
    }
}
