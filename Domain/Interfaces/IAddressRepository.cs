namespace ApiPetShop.Domain;

public interface IAddressRepository
{
    Task<int> Create(AddressModel address);
    void Update(AddressModel address);
    Task<AddressModel> GetAddress();
}