using ApiPetShop.Infra;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class VetRepository(DbConnectionContext db, IMapper mapper) : IVetRepository
    {
        private readonly DbConnectionContext _db = db;
        private readonly IMapper _mapper = mapper;
        //Todo: Adicionar o método de relação entre vacina e servico
        //Todo: adicionar paginação
        public async Task<List<VetServiceDto>> GetAllPetServices() => _mapper.Map<List<VetServiceDto>>(await _db.VetServices.AsNoTracking().ToListAsync());
        public async Task<List<DateTime>> GetScheduledTime(DateTime date) => await _db.VetServices.AsNoTracking().Where(x => x.ScheduledDate.Day == date.Day && x.ScheduledDate.Month == date.Month && x.ScheduledDate.Year == date.Year).Select(x => x.ScheduledDate).ToListAsync();
        public async Task<VetServiceDto> GetServiceByIdDto(int id) => _mapper.Map<VetServiceDto>(await _db.VetServices.AsNoTracking().Include(x => x.Vacines).FirstOrDefaultAsync(x => x.Id == id)) ?? new();
        public async Task<VetServiceModel> GetServiceById(int id) => await _db.VetServices.FindAsync(id) ?? new();

        public async Task AddRelWithVacine(List<VetVacine> relations)
        {
            await _db.VetVacines.AddRangeAsync(relations);
            await _db.SaveChangesAsync();
        }

        public async Task CreateService(VetServiceModel service)
        {
            await _db.VetServices.AddAsync(service);
            await _db.SaveChangesAsync();
        }

        public void Update(VetServiceModel service)
        {
            _db.VetServices.Update(service);
            _db.SaveChanges();
        }
    }
}
