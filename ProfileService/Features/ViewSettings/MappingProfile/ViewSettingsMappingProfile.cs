using AutoMapper;
using ProfileService.Domain.Entities;
using ProfileService.Features.ViewSettings.DTOs;
using ProfileService.Features.ViewSettings.ViewModels;

namespace ProfileService.Features.ViewSettings.MappingProfile
{
    public class ViewSettingsMappingProfile : Profile
    {
        public ViewSettingsMappingProfile()
        {
            CreateMap<UserPreferences, UserPreferencesDto>();
            CreateMap<NotificationSettings, NotificationSettingsDto>();
            CreateMap<PrivacySettings, PrivacySettingsDto>();

            CreateMap<UserPreferencesDto, UserPreferencesViewModel>();
            CreateMap<NotificationSettingsDto, NotificationSettingsViewModel>();
            CreateMap<PrivacySettingsDto, PrivacySettingsViewModel>();

            CreateMap<UserSettingsDto, ViewSettingsResponseViewModel>();
        }
    }
}
