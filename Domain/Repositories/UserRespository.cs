using ApiPetShop.Infra;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class UserRespository(DbConnectionContext db, IMapper mapper) : IUserRepository
    {
        private readonly DbConnectionContext _db = db;
        private readonly IMapper _mapper = mapper;

        public async Task<List<UserDto>> GetAllUsers() => _mapper.Map<List<UserDto>>(await _db.Users.ToListAsync());
        public async Task<UserModel> GetUserById(int id) => await _db.Users.FindAsync(id) ?? new();
        public async Task<UserDto> GetUserByIdDto(int id) => _mapper.Map<UserDto>(await _db.Users.FindAsync(id)) ?? new();
        public async Task<UserModel> GetUserByEmail(string email) => await _db.Users.Where(x => x.Email == email).FirstOrDefaultAsync() ?? new();

        public async Task CreateUser(UserModel user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public void UpdateUser(UserModel user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }
    }
}
