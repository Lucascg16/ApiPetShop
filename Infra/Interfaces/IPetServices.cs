using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface IPetServices
    {
        Task CreateService(PetServiceModel service);
        Task<List<PetServiceModel>> GetAllPetServices();
        Task<PetServiceModel> GetServiceById(int id);
        void Update(PetServiceModel service);
    }
}
