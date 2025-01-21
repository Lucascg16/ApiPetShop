namespace ApiPetShop.Domain.Models
{
    public class VacineModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<VetServiceModel> VetServices { get; set; } = [];
    }
}
