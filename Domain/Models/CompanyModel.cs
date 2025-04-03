using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiPetShop.Domain;

public class CompanyModel : ModelBase
{
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    public string ContactEmail { get; set; } = string.Empty;
    public int AddressId { get; set; }
    [JsonIgnore]
    public AddressModel Address { get; set; } = new(); 
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;
    [MaxLength(512)]
    public string InstagramAddress { get; set; } = string.Empty;

    public CompanyModel(string name, string contactEmail, int addressId, string phoneNumber, string instagramAddress)
    {
        Name = name;
        ContactEmail = contactEmail;
        AddressId = addressId;
        PhoneNumber = phoneNumber;
        InstagramAddress = instagramAddress;
    }
    
    public CompanyModel() {}

    public void UpdateCompany(CreateOrUpdateCompanyModel newCompany)
    {
        Name = newCompany.Name;
        Address = newCompany.Address;
        PhoneNumber = newCompany.PhoneNumber;
        InstagramAddress = newCompany.InstagramAddress;
        UpdateModel();
    }
}