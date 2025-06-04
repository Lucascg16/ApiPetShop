using ApiPetShop.Infra;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class CustumerRepository(DbConnectionContext db) : ICustumerRepository
    {
        public async Task Create(string email)
        {
            await db.Custumers.AddAsync(new(email));
            await db.SaveChangesAsync();
        }

        public async Task<CustumerModel> Get(string email)
        {
            return await db.Custumers.FirstOrDefaultAsync(x => x.Email == email) ?? new();
        }

        public async Task<List<CustumerModel>> GetAll()
        {
            return await db.Custumers.Where(x => !x.IsDeleted).ToListAsync() ?? [];
        }
    }
}