using AutoMapper;

namespace ApiPetShop.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserDto, UserModel>().ReverseMap();
            CreateMap<PetServiceDto, PetServiceModel>().ReverseMap();
            CreateMap<PetServiceListDto, PetServiceModel>().ReverseMap();
            CreateMap<VetServiceDto, VetServiceModel>().ReverseMap();
            CreateMap<VetServiceListDto, VetServiceModel>().ReverseMap();
            CreateMap<CompanyDto, CompanyModel>().ReverseMap();
        }
    }
}
