using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Domain;

public record ResetPasswordModel
{
    [Required(ErrorMessage = "Id é obrigatório")]
    public int Id { get; init; }
    [Required(ErrorMessage = "A senha é obrigatória")] 
    public string? Password { get; init; }
};