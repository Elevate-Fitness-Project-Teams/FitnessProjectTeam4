using AutoMapper;
using ProfileService.Features.UploadProfilePicture.ViewModels;

namespace ProfileService.Features.UploadProfilePicture.MappingProfile
{
    public class UploadProfilePictureMappingProfile : Profile
    {
        public UploadProfilePictureMappingProfile()
        {
            CreateMap<string, UploadProfilePictureResponseViewModel>()
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src));
        }
    }
}
