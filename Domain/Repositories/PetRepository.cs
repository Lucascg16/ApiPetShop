using ApiPetShop.Infra;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class PetRepository(DbConnectionContext db) : IPetRepository
    {
        private readonly DbConnectionContext _db = db;
        public async Task CreateService(PetServiceModel service)
        {
            await _db.PetServices.AddAsync(service);
        }

        public async Task<List<PetServiceModel>> GetAllPetServices()
        {
            return await _db.PetServices.ToListAsync();
        }

        public async Task<PetServiceModel> GetServiceById(int id)
        {
            return await _db.PetServices.FindAsync(id) ?? new();
        }

        public void Update(PetServiceModel service)
        {
            _db.PetServices.Update(service);
        }
    }
}
