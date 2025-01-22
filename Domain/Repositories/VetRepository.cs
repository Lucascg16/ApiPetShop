using ApiPetShop.Infra;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class VetRepository(DbConnectionContext db) : IVetRepository
    {
        private readonly DbConnectionContext _db = db;
        public async Task CreateService(VetServiceModel service)
        {
            await _db.VetServices.AddAsync(service);
            await _db.SaveChangesAsync();
        }

        public async Task<List<VetServiceModel>> GetAllPetServices()
        {
            return await _db.VetServices.ToListAsync();
        }

        public async Task<VetServiceModel> GetServiceById(int id)
        {
            return await _db.VetServices.FindAsync(id) ?? new();
        }

        public void Update(VetServiceModel service)
        {
            _db.VetServices.Update(service);
            _db.SaveChanges();
        }
    }
}
