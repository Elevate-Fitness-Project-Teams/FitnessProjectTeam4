using AutoMapper;
using ProfileService.Domain.Entities;
using ProfileService.Features.ViewProfile.DTOs;
using ProfileService.Features.ViewProfile.ViewModels;

namespace ProfileService.Features.ViewProfile.Mapping
{
    public class ViewProfileMappingProfile : Profile
    {
        public ViewProfileMappingProfile()
        {
            
            CreateMap<UserProfile, ProfileDataDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<UserStatisticCache, ProfileStatisticsDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            
            CreateMap<ProfileDataDto, ProfileViewModel>();

            CreateMap<ProfileStatisticsDto, ProfileViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.FirstName, opt => opt.Ignore())
                .ForMember(dest => dest.LastName, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.Ignore())
                .ForMember(dest => dest.IsPremiumCached, opt => opt.Ignore());
        }
    }
}
