namespace ApiPetShop.Domain
{
    public record VacineModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<VetServiceModel> VetServices { get; set; } = [];
    }
}
