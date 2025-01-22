namespace ApiPetShop.Domain
{
    public interface IUserRepository
    {
        Task CreateUser(UserModel user);
        void UpdateUser(UserModel user);
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(int id);
    }
}
