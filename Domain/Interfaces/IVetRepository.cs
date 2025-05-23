﻿namespace ApiPetShop.Domain
{
    public interface IVetRepository
    {
        Task<int> CreateService(VetServiceModel service);
        Task ManageRelWithVacine(List<VetVacine> newRelations);
        Task<List<VetServiceDto>> GetAllPetServices();
        Task<List<DateTime>> GetScheduledTime(DateTime date);
        Task<List<ServiceListDto>> GetByDate(DateTime date);
        Task<VetServiceDto> GetServiceByIdDto(int id);
        Task<VetServiceModel> GetServiceById(int id);
        void Update(VetServiceModel service);
        
    }
}
