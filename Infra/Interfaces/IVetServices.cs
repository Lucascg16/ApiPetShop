using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface IVetServices
    {
        Task CreateService(VetServiceModel service);
        Task<List<VetServiceModel>> GetAllPetServices();
        Task<VetServiceModel> GetServiceById(int id);
        void Update(VetServiceModel service);
    }
}
