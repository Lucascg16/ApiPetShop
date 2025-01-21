using ApiPetShop.Domain.Enum;

namespace ApiPetShop.Domain.Models
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

        public UserModel(){ }

        public void UpdateUser(string fName, string lName, string email)
        {
            FirstName = fName;
            LastName = lName;
            Email = email;
            UpdateModel();
        }

        public void UpdatePassWord(string pass)
        {
            Password = pass;
            UpdateModel();
        }

        public void UpdateRole(UserRoleEnum nRole)
        {
            Role = nRole;
            UpdateModel();
        }

        public void DeleteUser() 
        {
            DeleteFlag();
        }
    }
}
