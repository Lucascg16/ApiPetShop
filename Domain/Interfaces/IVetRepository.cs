﻿namespace ApiPetShop.Domain
{
    public interface IVetRepository
    {
        Task CreateService(VetServiceModel service);
        Task ManageRelWithVacine(List<VetVacine> newRelations);
        Task<List<VetServiceDto>> GetAllPetServices();
        Task<List<DateTime>> GetScheduledTime(DateTime date);
        Task<List<VetServiceListDto>> GetByDate(DateTime date);
        Task<VetServiceDto> GetServiceByIdDto(int id);
        Task<VetServiceModel> GetServiceById(int id);
        void Update(VetServiceModel service);
        
    }
}
