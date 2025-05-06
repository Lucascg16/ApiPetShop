namespace ApiPetShop.Domain
{
    public interface IUserRepository
    {
        Task CreateUser(UserModel user);
        void UpdateUser(UserModel user);
        Task<List<UserDto>> GetAllUsers(string? name);
        Task<UserModel> GetUserById(int id);
        Task<UserModel> GetUserByEmail(string email);
        Task<UserDto> GetUserByIdDto(int id);
    }
}
