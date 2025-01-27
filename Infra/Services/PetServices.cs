using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public class PetServices(IPetRepository repository) : IPetServices
    {
        private readonly IPetRepository _repository = repository;

        public async Task<List<PetServiceDto>> GetAllPetServices() => await _repository.GetAllPetServices();
        public async Task<PetServiceDto> GetServiceById(int id) => await _repository.GetServiceByIdDto(id);
        public async Task CreateService(CreatePetServiceModel service) => await _repository.CreateService(new(service));

        public async Task Update(UpdatePetService nService)
        {
            var service = await _repository.GetServiceById(nService.Id);
            service.UpdateService(nService);
            _repository.Update(service);
        }

        public async Task Delete(int id)
        {
            var service = await _repository.GetServiceById(id);
            service.DeletePetService();
            _repository.Update(service);
        }
    }
}
