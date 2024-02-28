using AutoMapper;
using dating_backend.DTOs;
using dating_backend.Entities;

namespace dating_backend.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, MemberDto>().ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url)); // Map to go from User to MemberDTO.
            CreateMap<Photo, PhotoDto>(); // Map to go from Photo to PhotoDTO.
        }
    }
}
