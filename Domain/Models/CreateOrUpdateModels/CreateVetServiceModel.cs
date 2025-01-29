using ApiPetShop.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ApiPetShop.Domain
{
    public record CreateVetServiceModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsWhatsApp { get; set; } = false;
        [Required]
        public string PetName { get; set; } = string.Empty;
        [Required]
        public int PetAge { get; set; }
        [Required]
        public PetTypeEnum Type { get; set; } = PetTypeEnum.none;
        [Required]
        public PetGenderEnum PetGender { get; set; } = PetGenderEnum.Male;
        [Required]
        public PetSizeEnum PetSize { get; set; } = PetSizeEnum.Small;
        [Required]
        public DateTime ScheduledDate { get; set; }
        public double PetWeight { get; set; }
        public bool IsCastrated { get; set; } = false;
        //public List<VacineModel> Vacines { get; set; } = [];
    }
}
