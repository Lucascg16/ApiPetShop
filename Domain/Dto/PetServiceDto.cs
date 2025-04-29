using ApiPetShop.Domain.Enum;

namespace ApiPetShop.Domain
{
    public record PetServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsWhatsApp { get; set; }
        public string PetName { get; set; } = string.Empty;
        public int PetAge { get; set; }
        public PetTypeEnum PetType { get; set; }
        public PetGenderEnum PetGender { get; set; }
        public PetSizeEnum PetSize { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
}
