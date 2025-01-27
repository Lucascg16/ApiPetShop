using ApiPetShop.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Domain
{
    public record CreateUserModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public UserRoleEnum Role { get; set; } = UserRoleEnum.None;
    }
}
