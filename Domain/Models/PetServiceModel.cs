using ApiPetShop.Domain.Enum;

namespace ApiPetShop.Domain
{
    public class PetServiceModel : ModelBase
    {
        public PetServiceModel(string name, string? email, string? phoneNumber,
            bool isWhatsApp, string petName, int petAge, PetTypeEnum type,
            PetGenderEnum petGender, PetSizeEnum petSize, DateTime scheduledDate)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            IsWhatsApp = isWhatsApp;
            PetName = petName;
            PetAge = petAge;
            PetType = type;
            PetGender = petGender;
            PetSize = petSize;
            ScheduledDate = scheduledDate;
        }

        public PetServiceModel(CreateOrUpdatePetService nPetservice)
        {
            Name = nPetservice.Name;
            Email = nPetservice.Email ?? "";
            PhoneNumber = nPetservice.PhoneNumber ?? "";
            IsWhatsApp = nPetservice.IsWhatsApp;
            PetName = nPetservice.PetName;
            PetAge = nPetservice.PetAge;
            PetType = nPetservice.PetType;
            PetGender = nPetservice.PetGender;
            PetSize = nPetservice.PetSize;
            ScheduledDate = nPetservice.ScheduledDate;
        }

        public PetServiceModel(){ }

        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsWhatsApp { get; set; } = false;
        public string PetName { get; set; } = string.Empty;
        public int PetAge { get; set; }
        public PetTypeEnum PetType {  get; set; }
        public PetGenderEnum PetGender { get; set; }
        public PetSizeEnum PetSize { get; set; }
        public DateTime ScheduledDate {  get; set; }
        
        public void UpdateService(CreateOrUpdatePetService nService)
        {
            Name = nService.Name;
            Email = nService.Email;
            PhoneNumber = nService.PhoneNumber;
            IsWhatsApp = nService.IsWhatsApp;
            PetName = nService.PetName;
            PetAge = nService.PetAge;
            PetType = nService.PetType;
            PetGender = nService.PetGender;
            PetSize = nService.PetSize;
            ScheduledDate = nService.ScheduledDate;
        }
    }
}
