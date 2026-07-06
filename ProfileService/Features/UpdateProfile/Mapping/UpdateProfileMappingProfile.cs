using AutoMapper;
using ProfileService.Domain.Entities;
using ProfileService.Features.UpdateProfile.Commands;
using ProfileService.Features.UpdateProfile.ViewModels;

namespace ProfileService.Features.UpdateProfile.MappingProfile
{
    public class UpdateProfileMappingProfile : Profile
    {
        public UpdateProfileMappingProfile()
        {
            CreateMap<UpdateProfileRequestViewModel, UpdateProfileCommand>();

            CreateMap<UpdateProfileCommand, UserProfile>();
        }
    }
}
