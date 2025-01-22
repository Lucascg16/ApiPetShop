using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public class VetServices(IVetRepository repository) : IVetServices
    {
        private readonly IVetRepository _repository = repository;

        public async Task CreateService(VetServiceModel service)
        {
            await _repository.CreateService(service);
        }

        public async Task<List<VetServiceModel>> GetAllPetServices()
        {
            return await _repository.GetAllPetServices();
        }

        public async Task<VetServiceModel> GetServiceById(int id)
        {
            return await _repository.GetServiceById(id);
        }

        public void Update(VetServiceModel service)
        {
            _repository.Update(service);
        }
    }
}
