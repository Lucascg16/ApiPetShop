using ApiPetShop.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Domain
{
    public record UpdatePetService
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsWhatsApp { get; set; } = false;
        [Required]
        public string PetName { get; set; } = string.Empty;
        [Required]
        public PetGenderEnum PetGender { get; set; } = PetGenderEnum.Male;
        [Required]
        public PetSizeEnum PetSize { get; set; } = PetSizeEnum.Small;
        [Required]
        public DateTime ScheduledDate { get; set; }
    }
}
