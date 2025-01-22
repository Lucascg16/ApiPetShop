using ApiPetShop.Domain;

namespace ApiPetShop.Infra.Services
{
    public class PetServices(IPetRepository repository) : IPetServices
    {
        private readonly IPetRepository _repository = repository;

        public async Task CreateService(PetServiceModel service)
        {
            await _repository.CreateService(service);
        }

        public async Task<List<PetServiceModel>> GetAllPetServices()
        {
            return await _repository.GetAllPetServices();
        }

        public async Task<PetServiceModel> GetServiceById(int id)
        {
            return await _repository.GetServiceById(id);
        }

        public void Update(PetServiceModel service)
        {
            _repository.Update(service);
        }
    }
}
