using ApiPetShop.Infra;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class UserRespository(DbConnectionContext db, IMapper mapper) : IUserRepository
    {
        public async Task<UserModel> GetUserById(int id) => await db.Users.FirstOrDefaultAsync(x => x.Id == id) ?? new();
        public async Task<UserDto> GetUserByIdDto(int id) => mapper.Map<UserDto>(await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)) ?? new();
        public async Task<UserModel> GetUserByEmail(string email) => await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email) ?? new();

        public async Task<List<UserDto>> GetAllUsers(string? name){
            var users = await db.Users.AsNoTracking().ToListAsync();

            if(!string.IsNullOrEmpty(name)){
                users = users.Where(x => x.FirstName.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            return mapper.Map<List<UserDto>>(users);
        }

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
