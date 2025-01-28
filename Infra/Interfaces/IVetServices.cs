using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface IVetServices
    {
        Task CreateService(CreateVetServiceModel service);
        Task<List<VetServiceDto>> GetAllPetServices();
        Task<VetServiceDto> GetServiceByIdDto(int id);
        Task Update(UpdateVetserviceModel service);
        Task Delete(int id);
    }
}
