namespace ApiPetShop.Domain.Models
{
    public record CustomEmailModel
    {
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}