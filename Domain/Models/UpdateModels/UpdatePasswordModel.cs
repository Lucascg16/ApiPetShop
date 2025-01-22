using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Domain
{
    public record UpdatePasswordModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "A nova senha é obrigatória")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
