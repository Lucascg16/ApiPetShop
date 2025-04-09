namespace ApiPetShop.Domain
{
    public interface IPetRepository
    {
        Task CreateService(PetServiceModel service);
        Task<List<PetServiceDto>> GetAllPetServices();
        Task<List<DateTime>> GetScheduledTime(DateTime date);
        Task<List<ServiceListDto>> GetByDate(DateTime date);
        Task<PetServiceDto> GetServiceByIdDto(int id);
        Task<PetServiceModel> GetServiceById(int id);
        void Update(PetServiceModel service);
    }
}
