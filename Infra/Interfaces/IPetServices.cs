using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface IPetServices
    {
        Task CreateService(CreateOrUpdatePetService service);
        Task<List<PetServiceDto>> GetAllPetServices();
        Task<PetServiceDto> GetServiceById(int id);
        Task<List<ServiceListDto>> GetByDate(DateTime date);
        Task Update(CreateOrUpdatePetService nService);
        Task Delete(int id);
    }
}
