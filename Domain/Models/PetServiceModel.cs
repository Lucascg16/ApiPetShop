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
            Type = type;
            PetGender = petGender;
            PetSize = petSize;
            ScheduledDate = scheduledDate;
        }

        public PetServiceModel(CreatePetServiceModel nPetservice)
        {
            Name = nPetservice.Name;
            Email = nPetservice.Email ?? "";
            PhoneNumber = nPetservice.PhoneNumber ?? "";
            IsWhatsApp = nPetservice.IsWhatsApp;
            PetName = nPetservice.PetName;
            PetAge = nPetservice.PetAge;
            Type = nPetservice.Type;
            PetGender = nPetservice.PetGender;
            PetSize = nPetservice.PetSize;
            ScheduledDate = nPetservice.ScheduledDate;
        }

        public PetServiceModel(){ }

        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsWhatsApp { get; set; }
        public string PetName { get; set; } = string.Empty;
        public int PetAge { get; set; }
        public PetTypeEnum Type {  get; set; }
        public PetGenderEnum PetGender { get; set; }
        public PetSizeEnum PetSize { get; set; }
        public DateTime ScheduledDate {  get; set; }
        
        public void UpdateService(UpdatePetService nService)
        {
            Name = nService.Name;
            Email = nService.Email;
            PhoneNumber = nService.PhoneNumber;
            IsWhatsApp = nService.IsWhatsApp;
            PetName = nService.PetName;
            PetGender = nService.PetGender;
            PetSize = nService.PetSize;
            ScheduledDate = nService.ScheduledDate;
        }
    }
}
