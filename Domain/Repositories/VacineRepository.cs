using ApiPetShop.Infra;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class VacineRepository(DbConnectionContext db) : IVacineRepository
    {
        private readonly DbConnectionContext _db = db;
        public async Task<List<VacineModel>> GetAll() => await _db.Vacines.ToListAsync();
    }
}
