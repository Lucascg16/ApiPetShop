using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public class UserServices(IUserRepository repo) : IUserServices
    {
        private readonly IUserRepository _repository = repo;

        public async Task CreateUser(CreateUserModel nUser)
        {
            UserModel user = new(nUser);
            await _repository.CreateUser(user);
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            return await _repository.GetAllUsers();
        }

        public Task<UserDto> GetUserByIdDto(int id)
        {
            return _repository.GetUserByIdDto(id);
        }

        public async Task UpdateUser(UpdateUserModel nUser)
        {
            var user = await _repository.GetUserById(nUser.Id);
            user.UpdateUser(nUser);
            user.UpdateRole(user.Role);

            _repository.UpdateUser(user);
        }

        public async Task UpdatePassword(UpdatePasswordModel update)
        {
            var user = await _repository.GetUserById(update.Id);

            if (user.Id == 0) throw new("Usuário não encontrado");
            if (update.Password != user.Password) throw new("A senha digitada não confere com a senha atual");
            
            user.UpdatePassword(update.NewPassword);
            _repository.UpdateUser(user);
        }

        public async Task DeleteUser(int id)
        {
            var user = await _repository.GetUserById(id);
            if (user.Id == 0) throw new("Usuário não encontrado");

            user.DeleteUser();
            _repository.UpdateUser(user);
        }
    }
}
    