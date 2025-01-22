namespace ApiPetShop.Domain
{
    public interface IPetRepository
    {
        Task CreateService(PetServiceModel service);
        Task<List<PetServiceModel>> GetAllPetServices();
        Task<PetServiceModel> GetServiceById(int id);
        void Update(PetServiceModel service);
    }
}
