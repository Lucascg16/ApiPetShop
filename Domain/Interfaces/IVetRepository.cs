namespace ApiPetShop.Domain
{
    public interface IVetRepository
    {
        Task CreateService(VetServiceModel service);
        Task<List<VetServiceDto>> GetAllPetServices();
        Task<VetServiceDto> GetServiceByIdDto(int id);
        Task<VetServiceModel> GetServiceById(int id);
        void Update(VetServiceModel service);
    }
}
