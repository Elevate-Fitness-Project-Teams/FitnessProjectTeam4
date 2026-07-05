using AutoMapper;
using ProfileService.Domain.Entities;
using ProfileService.Features.UpdateSettings.DTOs;
using ProfileService.Features.UpdateSettings.ViewModels;

namespace ProfileService.Features.UpdateSettings.MappingProfile
{
    public class UpdateSettingsMappingProfile : Profile
    {
        public UpdateSettingsMappingProfile()
        {
           
            CreateMap<UpdatePreferencesViewModel, UpdatePreferencesDto>();
            CreateMap<UpdateNotificationsViewModel, UpdateNotificationsDto>();
            CreateMap<UpdatePrivacyViewModel, UpdatePrivacyDto>();
            CreateMap<UpdateSettingsRequestViewModel, UpdateSettingsDto>();


            CreateMap<UpdatePreferencesDto, UserPreferences>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateNotificationsDto, NotificationSettings>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdatePrivacyDto, PrivacySettings>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
