using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public class UserServices(IUserRepository repository, ICryptoService cryptoService, IEmailService emailService) : IUserServices
    {
        public async Task<List<UserDto>> GetAllUsers(string? name) => await repository.GetAllUsers(name);
        public async Task<UserDto> GetUserByIdDto(int id) => await repository.GetUserByIdDto(id);
        public async Task<UserModel> GetUserById(int id) => await repository.GetUserById(id);
        public async Task<UserModel> GetUserByEmail(string email) => await repository.GetUserByEmail(email);

        public async Task CreateUser(CreateOrUpdateUserModel nUser)
        {
            if (nUser.Id != 0) throw new("O usuário já existe");

            UserModel user = new(nUser);
            user.Password = user.GenerateTempPassword();
            await emailService.SendCustomEmail(user.Email, "Criação de conta", EmailTemplates.CriarConta.Replace("{password}", $"{user.Password}"));

            user.Password = cryptoService.Encrypt(user.Password);
            await repository.CreateUser(user);
        }

        public async Task UpdateUser(CreateOrUpdateUserModel nUser)
        {
            var user = await repository.GetUserById(nUser.Id);
            if (user.Id == 0) throw new("Usuário não econtrado");

            user.UpdateUser(nUser);
            user.UpdateRole(nUser.Role);

            repository.UpdateUser(user);
        }

        public async Task UpdatePassword(UpdatePasswordModel update)
        {
            var user = await repository.GetUserById(update.Id);

            if (user.Id == 0) throw new("Usuário não encontrado");
            if (update.Password != cryptoService.Decrypt(user.Password)) throw new("A senha digitada não confere com a senha atual");

            user.UpdatePassword(cryptoService.Encrypt(update.NewPassword));
            repository.UpdateUser(user);
        }

        public async Task ResetPassword(ResetPasswordModel model)
        {
            var user = await repository.GetUserById(model.Id);
            if (user.Id == 0) throw new("Usuário não encontrado");

            user.UpdatePassword(cryptoService.Encrypt(model.Password ?? ""));
            repository.UpdateUser(user);
        }

        public async Task DeleteUser(int id)
        {
            var user = await repository.GetUserById(id);
            if (user.Id == 0) throw new("Usuário não encontrado");

            user.Delete();
            repository.UpdateUser(user);
        }
    }
}
