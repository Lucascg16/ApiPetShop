using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Domain
{
    public record LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "O endereço de email digitado não é válido")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
