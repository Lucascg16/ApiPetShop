using ApiPetShop.Infra;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class PetRepository(DbConnectionContext db, IMapper mapper) : IPetRepository
    {
        private readonly DbConnectionContext _db = db;
        private readonly IMapper _mapper = mapper;

        public async Task<List<PetServiceDto>> GetAllPetServices() => _mapper.Map<List<PetServiceDto>>(await _db.PetServices.ToListAsync());
        public async Task<List<DateTime>> GetScheduledTime(DateTime date) => await _db.PetServices.Where(x => x.ScheduledDate.Day == date.Day && x.ScheduledDate.Month == date.Month && x.ScheduledDate.Year == date.Year).Select(x => x.ScheduledDate).ToListAsync() ?? [];
        public async Task<PetServiceDto> GetServiceByIdDto(int id) => _mapper.Map<PetServiceDto>(await _db.PetServices.FindAsync(id)) ?? new();
        public async Task<PetServiceModel> GetServiceById(int id) => await _db.PetServices.FindAsync(id) ?? new();

        public async Task CreateService(PetServiceModel service)
        {
            await _db.PetServices.AddAsync(service);
            await _db.SaveChangesAsync();
        }

        public void Update(PetServiceModel service)
        {
            _db.PetServices.Update(service);
            _db.SaveChanges();
        }
    }
}
