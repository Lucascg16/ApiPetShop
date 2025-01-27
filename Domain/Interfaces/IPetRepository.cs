namespace ApiPetShop.Domain
{
    public interface IPetRepository
    {
        Task CreateService(PetServiceModel service);
        Task<List<PetServiceDto>> GetAllPetServices();
        Task<PetServiceDto> GetServiceByIdDto(int id);
        Task<PetServiceModel> GetServiceById(int id);
        void Update(PetServiceModel service);
    }
}
