namespace ApiPetShop.Domain;

public record CompanyDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? ContactEmail { get; set; }
    public AddressModel Address { get; init; } = new();
    public string? PhoneNumber { get; init; }
    public string? InstagramAddress { get; init; }
}