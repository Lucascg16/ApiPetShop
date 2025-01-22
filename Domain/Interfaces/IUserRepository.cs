namespace ApiPetShop.Domain
{
    public interface IUserRepository
    {
        Task CreateUser(UserModel user);
        void UpdateUser(UserModel user);
        Task<List<UserDto>> GetAllUsers();
        Task<UserModel> GetUserById(int id);
        Task<UserDto> GetUserByIdDto(int id);
    }
}
