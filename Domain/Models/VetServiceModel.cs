using ApiPetShop.Domain.Enum;
using System.Text.Json.Serialization;

namespace ApiPetShop.Domain
{
    public class VetServiceModel : ModelBase
    {
        public VetServiceModel(string name, string? email, string? phoneNumber, bool isWhatsApp, string petName,
            int petAge, PetTypeEnum type, PetGenderEnum petGender, PetSizeEnum petSize, 
            DateTime scheduledDate, double petWeight, bool isCastrated)
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
            PetWeight = petWeight;
            IsCastrated = isCastrated;
        }

        public VetServiceModel(CreateVetServiceModel nService)
        {
            Name = nService.Name;
            Email = nService.Email;
            PhoneNumber = nService.PhoneNumber;
            IsWhatsApp = nService.IsWhatsApp;
            PetName = nService.PetName;
            PetAge = nService.PetAge;
            Type = nService.Type;
            PetGender = nService.PetGender;
            PetSize = nService.PetSize;
            ScheduledDate = nService.ScheduledDate;
            PetWeight = nService.PetWeight;
            IsCastrated = nService.IsCastrated;
        }

        public VetServiceModel(){ }

        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsWhatsApp { get; set; } = false;
        public string PetName { get; set; } = string.Empty;
        public int PetAge { get; set; }
        public PetTypeEnum Type { get; set; }
        public PetGenderEnum PetGender { get; set; }
        public PetSizeEnum PetSize { get; set; }
        public DateTime ScheduledDate { get; set; }
        public double PetWeight { get; set; }
        public bool IsCastrated { get; set; }
        [JsonIgnore]
        public List<VacineModel> Vacines { get; set; } = [];

        public void UpdateService(UpdateVetserviceModel nService)
        {
            Name = nService.Name;
            Email = nService.Email;
            PhoneNumber = nService.PhoneNumber;
            IsWhatsApp = nService.IsWhatsApp;
            PetName = nService.PetName;
            PetGender = nService.PetGender;
            PetSize = nService.PetSize;
            ScheduledDate = nService.ScheduledDate;
            PetWeight = nService.PetWeight;
            IsCastrated = nService.IsCastrated;
        }
    }
}
