using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface IUserServices
    {
        Task CreateUser(CreateUserModel nUser);
        Task UpdateUser(UpdateUserModel user);
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserByIdDto(int id);
        Task<UserModel> GetUserById(int id);
        Task<UserModel> GetUserByEmail(string email);
        Task UpdatePassword(UpdatePasswordModel update);
        Task ResetPassword(ResetPasswordModel model);
        Task DeleteUser(int id);
    }
}
