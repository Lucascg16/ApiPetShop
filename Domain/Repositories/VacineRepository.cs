using ApiPetShop.Infra;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class VacineRepository(Infra.DbConnectionContext db) : IVacineRepository
    {
        private readonly Infra.DbConnectionContext _db = db;

        public async Task<List<VacineModel>> GetAll()
        {
            return await _db.Vacines.ToListAsync();
        }
    }
}
