namespace ApiPetShop.Domain
{
    public record VetVacine
    {
        public int VetServiceId { get; set; }
        public Guid VacineId { get; set; }
    }
}
