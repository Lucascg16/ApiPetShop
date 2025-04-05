using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface IPetServices
    {
        Task CreateService(CreatePetServiceModel service);
        Task<List<PetServiceDto>> GetAllPetServices();
        Task<PetServiceDto> GetServiceById(int id);
        Task<List<PetServiceDto>> GetByDate(DateTime date);
        Task Update(UpdatePetService nService);
        Task Delete(int id);
    }
}
