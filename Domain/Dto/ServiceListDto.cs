using ApiPetShop.Domain.Enum;

namespace ApiPetShop.Domain
{
    public record ServiceListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string PetName { get; set; } = string.Empty;
        public DateTime ScheduledDate { get; set; }
        public PetTypeEnum PetType { get; set; } = PetTypeEnum.none;
    }
}
