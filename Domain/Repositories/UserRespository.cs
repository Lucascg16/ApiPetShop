using ApiPetShop.Infra;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain
{
    public class UserRespository(DbConnectionContext db, IMapper mapper) : IUserRepository
    {
        private readonly DbConnectionContext _db = db;
        private readonly IMapper _mapper = mapper;

        public async Task CreateUser(UserModel user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllUsers()
        {            
            return _mapper.Map<List<UserDto>>(await _db.Users.ToListAsync()); ;
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return await _db.Users.FindAsync(id) ?? new();
        }

        public async Task<UserDto> GetUserByIdDto(int id)
        {
            return _mapper.Map<UserDto>(await _db.Users.FindAsync(id)) ?? new();
        }

        public void UpdateUser(UserModel user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }
    }
}
