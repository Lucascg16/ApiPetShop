using ApiPetShop.Infra;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class UserRespository(Infra.DbConnectionContext db) : IUserRepository
    {
        private readonly Infra.DbConnectionContext _db = db;

        public async Task CreateUser(UserModel user)
        {
            await _db.Users.AddAsync(user);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return await _db.Users.FindAsync(id) ?? new();
        }

        public void UpdateUser(UserModel user)
        {
            _db.Users.Update(user);
        }
    }
}
