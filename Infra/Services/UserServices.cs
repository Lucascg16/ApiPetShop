using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public class UserServices(IUserRepository repo, ICryptoService cryptoService) : IUserServices
    {
        private readonly IUserRepository _repository = repo;
        private readonly ICryptoService _cryptoService = cryptoService;

        public async Task<List<UserDto>> GetAllUsers() => await _repository.GetAllUsers();
        public async Task<UserDto> GetUserByIdDto(int id) => await _repository.GetUserByIdDto(id);
        public async Task<UserModel> GetUserById(int id) => await _repository.GetUserById(id);
        public async Task<UserModel> GetUserByEmail(string email) => await _repository.GetUserByEmail(email);

        public async Task CreateUser(CreateUserModel nUser)
        {
            UserModel user = new(nUser);
            user.Password = _cryptoService.Encrypt(user.Password);
            await _repository.CreateUser(user);
        }

        public async Task UpdateUser(UpdateUserModel nUser)
        {
            var user = await _repository.GetUserById(nUser.Id);
            if(user.Id == 0) throw new("Usuário não econtrado");

            user.UpdateUser(nUser);
            user.UpdateRole(user.Role);

            _repository.UpdateUser(user);
        }

        public async Task UpdatePassword(UpdatePasswordModel update)
        {
            var user = await _repository.GetUserById(update.Id);

            if (user.Id == 0) throw new("Usuário não encontrado");
            if (update.Password != _cryptoService.Decrypt(user.Password)) throw new("A senha digitada não confere com a senha atual");
            
            user.UpdatePassword(_cryptoService.Encrypt(update.NewPassword));
            _repository.UpdateUser(user);
        }

        public async Task DeleteUser(int id)
        {
            var user = await _repository.GetUserById(id);
            if (user.Id == 0) throw new("Usuário não encontrado");

            user.Delete();
            _repository.UpdateUser(user);
        }
    }
}
    