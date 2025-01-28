using ApiPetShop.Domain.Enum;

namespace ApiPetShop.Domain
{
    public class UserModel : ModelBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRoleEnum Role { get; set; } = UserRoleEnum.None;

        public UserModel(string firstName, string lastName, string email, string password, UserRoleEnum role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Role = role;
        }

        public UserModel(CreateUserModel user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Password = user.Password;
            Role = user.Role;
        }

        public UserModel(){ }

        public void UpdateUser(UpdateUserModel user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            UpdateModel();
        }

        public void UpdatePassword(string pass)
        {
            Password = pass;
            UpdateModel();
        }

        public void UpdateRole(UserRoleEnum nRole)
        {
            Role = nRole;
            UpdateModel();
        }
    }
}
