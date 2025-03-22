using ApiPetShop.Infra;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class UserRespository(DbConnectionContext db, IMapper mapper) : IUserRepository
    {
        public async Task<List<UserDto>> GetAllUsers() => mapper.Map<List<UserDto>>(await db.Users.AsNoTracking().ToListAsync());
        public async Task<UserModel> GetUserById(int id) => await db.Users.FindAsync(id) ?? new();
        public async Task<UserDto> GetUserByIdDto(int id) => mapper.Map<UserDto>(await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)) ?? new();
        public async Task<UserModel> GetUserByEmail(string email) => await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email) ?? new();

        public async Task CreateUser(UserModel user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public void UpdateUser(UserModel user)
        {
            db.Users.Update(user);
            db.SaveChanges();
        }
    }
}
