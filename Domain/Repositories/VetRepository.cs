using ApiPetShop.Infra;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class VetRepository(DbConnectionContext db, IMapper mapper) : IVetRepository
    {        
        public async Task<List<VetServiceDto>> GetAllPetServices() => mapper.Map<List<VetServiceDto>>(await db.VetServices.AsNoTracking().ToListAsync());
        public async Task<List<DateTime>> GetScheduledTime(DateTime date) => await db.VetServices.AsNoTracking().Where(x => x.ScheduledDate.DayOfYear == date.DayOfYear &&  x.ScheduledDate.Year == date.Year).Select(x => x.ScheduledDate).ToListAsync();
        public async Task<List<ServiceListDto>> GetByDate(DateTime date) => mapper.Map<List<ServiceListDto>>(await db.VetServices.AsNoTracking().Where(x => x.ScheduledDate.DayOfYear == date.DayOfYear && x.ScheduledDate.Year == date.Year).OrderBy(x => x.ScheduledDate).ToListAsync());
        public async Task<VetServiceDto> GetServiceByIdDto(int id) => mapper.Map<VetServiceDto>(await db.VetServices.AsNoTracking().Include(x => x.Vacines).FirstOrDefaultAsync(x => x.Id == id)) ?? new();
        public async Task<VetServiceModel> GetServiceById(int id) => await db.VetServices.FindAsync(id) ?? new();
        
        public async Task ManageRelWithVacine(List<VetVacine> newRelations)
        {
            var oldRelations = await db.VetVacines.Where(x => x.VetServiceId == newRelations[0].VetServiceId).ToListAsync();
            if (oldRelations.Count == 0)
            {
                await db.VetVacines.AddRangeAsync(newRelations);
                await db.SaveChangesAsync();   
                return;
            }
            
            var toRemove = oldRelations.Where(x => newRelations.All(n => n.VacineId != x.VacineId)).ToList();
            db.VetVacines.RemoveRange(toRemove);
                
            var toAdd = newRelations.Where(n => oldRelations.All(x => x.VacineId != n.VacineId)).ToList();
            db.VetVacines.AddRange(toAdd);
                
            await db.SaveChangesAsync();
        }

        public async Task<int> CreateService(VetServiceModel service)
        {
            await db.VetServices.AddAsync(service);
            await db.SaveChangesAsync();
            return service.Id;
        }

        public void Update(VetServiceModel service)
        {
            db.VetServices.Update(service);
            db.SaveChanges();
        }
    }
}
