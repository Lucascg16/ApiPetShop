﻿using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public class VetServices(IVetRepository repository) : IVetServices
    {
        public async Task<List<VetServiceDto>> GetAllPetServices() => await repository.GetAllPetServices();
        public async Task<VetServiceDto> GetServiceByIdDto(int id) => await repository.GetServiceByIdDto(id);
        public async Task CreateService(CreateVetServiceModel service) => await repository.CreateService(new(service));
        public async Task ManageRelWithVacine(List<VetVacine> newRelations) => await repository.ManageRelWithVacine(newRelations);
        public async Task<List<ServiceListDto>> GetByDate(DateTime date) => await repository.GetByDate(date);

        public async Task Update(UpdateVetserviceModel nService)
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
