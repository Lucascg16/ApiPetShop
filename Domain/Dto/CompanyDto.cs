namespace ApiPetShop.Domain;

public record CompanyDto
{
    public string? Name  { get; init; }
    public AddressModel? Address  { get; init; } 
    public string? PhoneNumber  { get; init; }
    public string? InstagramAddress   { get; init; }
}