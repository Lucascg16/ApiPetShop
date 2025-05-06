using System.ComponentModel.DataAnnotations;
using ApiPetShop.Domain.Enum;

namespace ApiPetShop.Domain
{
    public record CreateOrUpdateUserModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public UserRoleEnum Role { get; set; } = UserRoleEnum.None;
    }
}