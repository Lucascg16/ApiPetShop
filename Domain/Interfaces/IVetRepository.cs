namespace ApiPetShop.Domain
{
    public interface IVetRepository
    {
        Task CreateService(VetServiceModel service);
        Task<List<VetServiceModel>> GetAllPetServices();
        Task<VetServiceModel> GetServiceById(int id);
        void Update(VetServiceModel service);
    }
}
