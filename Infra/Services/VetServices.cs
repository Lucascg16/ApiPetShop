using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public class VetServices(IVetRepository repository) : IVetServices
    {
        private readonly IVetRepository _repository = repository;

        public async Task<List<VetServiceDto>> GetAllPetServices() => await _repository.GetAllPetServices();
        public async Task<VetServiceDto> GetServiceByIdDto(int id) => await _repository.GetServiceByIdDto(id);
        public async Task CreateService(CreateVetServiceModel service) => await _repository.CreateService(new(service));
        public async Task AddRelWithVacine(List<VetVacine> rel) => await _repository.AddRelWithVacine(rel);

        public async Task Update(UpdateVetserviceModel nService)
        {
            var service = await _repository.GetServiceById(nService.Id);
            if (service.Id == 0) throw new("Usuário não encontrado");

            service.UpdateService(nService);
            _repository.Update(service);
        }

        public async Task Delete(int id)
        {
            var service = await _repository.GetServiceById(id);
            if (service.Id == 0) throw new("Usuário não encontrado");

            service.Delete();
            _repository.Update(service);
        }
    }
}
