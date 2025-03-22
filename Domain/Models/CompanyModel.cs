using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiPetShop.Domain;

public class CompanyModel : ModelBase
{
    [MaxLength(255)]
    public string Name { get; set; }
    public int AddressId { get; set; }
    [JsonIgnore]
    public AddressModel Address { get; set; } 
    [MaxLength(20)]
    public string PhoneNumber { get; set; }
    [MaxLength(512)]
    public string InstagramAddress { get; set; }

    public CompanyModel(string name, int addressId, string phoneNumber, string instagramAddress)
    {
        Name = name;
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