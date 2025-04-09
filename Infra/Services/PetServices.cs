using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public class PetServices(IPetRepository repository) : IPetServices
    {
        public async Task<List<PetServiceDto>> GetAllPetServices() => await repository.GetAllPetServices();
        public async Task<PetServiceDto> GetServiceById(int id) => await repository.GetServiceByIdDto(id);
        public async Task CreateService(CreatePetServiceModel service) => await repository.CreateService(new(service));
        public async Task<List<ServiceListDto>> GetByDate(DateTime date) => await repository.GetByDate(date);

        public async Task Update(UpdatePetService nService)
        {
            var service = await repository.GetServiceById(nService.Id);
            if (service.Id == 0) throw new("Usuário não encontrado");

            service.UpdateService(nService);
            repository.Update(service);
        }

        public async Task Delete(int id)
        {
            var service = await repository.GetServiceById(id);
            if (service.Id == 0) throw new("Usuário não encontrado");

            service.Delete();
            repository.Update(service);
        }
    }
}
