using ApiPetShop.Domain.Enum;

namespace ApiPetShop.Domain.Models
{
    public class VetServiceModel : ModelBase
    {
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsWhatsApp { get; set; }
        public string PetName { get; set; } = string.Empty;
        public int PetAge { get; set; }
        public PetTypeEnum Type { get; set; }
        public PetGenderEnum PetGender { get; set; }
        public PetSizeEnum PetSize { get; set; }
        public DateTime ScheduledDate { get; set; }
        public double PetWeight { get; set; }
        public bool IsCastrated { get; set; }
        public List<VacineModel> Vacines { get; set; } = [];
    }
}
