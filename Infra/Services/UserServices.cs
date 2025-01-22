using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public class UserServices(IUserRepository repo) : IUserServices
    {
        private readonly IUserRepository _repository = repo;

        public async Task CreateUser(UserModel user)
        {
            await _repository.CreateUser(user);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _repository.GetAllUsers();
        }

        public Task<UserModel> GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }

        public void UpdateUser(UserModel user)
        {
            _repository.UpdateUser(user);
        }
    }
}
