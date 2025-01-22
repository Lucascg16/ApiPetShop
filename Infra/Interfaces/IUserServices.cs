using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface IUserServices
    {
        Task CreateUser(UserModel user);
        void UpdateUser(UserModel user);
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(int id);

    }
}
