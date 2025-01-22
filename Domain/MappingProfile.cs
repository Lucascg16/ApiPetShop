using AutoMapper;

namespace ApiPetShop.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserDto, UserModel>().ReverseMap();
        }
    }
}
