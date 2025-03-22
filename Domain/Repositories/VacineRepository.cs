using ApiPetShop.Infra;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class VacineRepository(DbConnectionContext db) : IVacineRepository
    {
        public async Task<List<VacineModel>> GetAll() => await db.Vacines.AsNoTracking().ToListAsync();
    }
}
