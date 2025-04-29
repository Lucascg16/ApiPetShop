using ApiPetShop.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Domain
{
    public record CreateOrUpdateVetserviceModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsWhatsApp { get; set; }
        [Required]
        public string PetName { get; set; } = string.Empty;
        [Required]
        public int PetAge { get; set; }
        [Required]
        public PetTypeEnum PetType { get; set; } = PetTypeEnum.none;
        [Required]
        public PetGenderEnum PetGender { get; set; }
        [Required]
        public PetSizeEnum PetSize { get; set; }
        [Required]
        public DateTime ScheduledDate { get; set; }
        [Required]
        public double PetWeight { get; set; }
        public bool IsCastrated { get; set; }
    }
}
