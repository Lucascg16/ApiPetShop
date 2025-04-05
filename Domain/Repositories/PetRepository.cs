using ApiPetShop.Infra;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class PetRepository(DbConnectionContext db, IMapper mapper) : IPetRepository
    {
        public async Task<List<PetServiceDto>> GetAllPetServices() => mapper.Map<List<PetServiceDto>>(await db.PetServices.AsNoTracking().ToListAsync());
        public async Task<List<DateTime>> GetScheduledTime(DateTime date) => await db.PetServices.AsNoTracking().Where(x => x.ScheduledDate.DayOfYear == date.DayOfYear && x.ScheduledDate.Year == date.Year).Select(x => x.ScheduledDate).ToListAsync();
        public async Task<List<PetServiceDto>> GetByDate(DateTime date) => mapper.Map<List<PetServiceDto>>(await db.PetServices.AsNoTracking().Where(x => x.ScheduledDate.DayOfYear == date.DayOfYear && x.ScheduledDate.Year == date.Year).ToListAsync());
        public async Task<PetServiceDto> GetServiceByIdDto(int id) => mapper.Map<PetServiceDto>(await db.PetServices.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)) ?? new();
        public async Task<PetServiceModel> GetServiceById(int id) => await db.PetServices.FindAsync(id) ?? new();

        public async Task CreateService(PetServiceModel service)
        {
            await db.PetServices.AddAsync(service);
            await db.SaveChangesAsync();
        }

        public void Update(PetServiceModel service)
        {
            db.PetServices.Update(service);
            db.SaveChanges();
        }
    }
}
