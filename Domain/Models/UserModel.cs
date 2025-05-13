using System.Text;
using ApiPetShop.Domain.Enum;

namespace ApiPetShop.Domain
{
    public class UserModel : ModelBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRoleEnum Role { get; set; } = UserRoleEnum.None;

        public UserModel(CreateOrUpdateUserModel user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Role = user.Role;
        }

        public UserModel() { }

        public string GenerateTempPassword()
        {
            string validar = "abcdefghijklmnozABCDEFGHIJKLMNOZ1234567890";
            int passlength = 8;
            StringBuilder strbld = new(100);
            Random random = new();
            
            while (0 < passlength--)
            {
                strbld.Append(validar[random.Next(validar.Length)]);
            }

            return strbld.ToString();
        }

        public void UpdateUser(CreateOrUpdateUserModel user)
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
