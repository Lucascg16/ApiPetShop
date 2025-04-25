using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface IVetServices
    {
        Task<int> CreateService(CreateOrUpdateVetserviceModel service);
        Task ManageRelWithVacine(List<VetVacine> newRelations);
        Task<List<ServiceListDto>> GetByDate(DateTime date);
        Task<List<VetServiceDto>> GetAllPetServices();
        Task<VetServiceDto> GetServiceByIdDto(int id);
        Task Update(CreateOrUpdateVetserviceModel service);
        Task Delete(int id);
    }
}
