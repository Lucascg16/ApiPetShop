using ApiPetShop.Domain.Enum;

namespace ApiPetShop.Domain
{
    public record UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRoleEnum Role { get; set; } = UserRoleEnum.None;
    }
}
