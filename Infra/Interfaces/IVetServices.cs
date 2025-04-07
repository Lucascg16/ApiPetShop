using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface IVetServices
    {
        Task CreateService(CreateVetServiceModel service);
        Task ManageRelWithVacine(List<VetVacine> newRelations);
        Task<List<VetServiceListDto>> GetByDate(DateTime date);
        Task<List<VetServiceDto>> GetAllPetServices();
        Task<VetServiceDto> GetServiceByIdDto(int id);
        Task Update(UpdateVetserviceModel service);
        Task Delete(int id);
    }
}
