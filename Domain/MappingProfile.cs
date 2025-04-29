using AutoMapper;

namespace ApiPetShop.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, UserModel>().ReverseMap();
            CreateMap<PetServiceDto, PetServiceModel>().ReverseMap();
            CreateMap<VetServiceDto, VetServiceModel>().ReverseMap();
            CreateMap<CompanyDto, CompanyModel>().ReverseMap();

            CreateMap<ServiceListDto, PetServiceModel>().ReverseMap();
            CreateMap<ServiceListDto, VetServiceModel>().ReverseMap();
        }
    }
}