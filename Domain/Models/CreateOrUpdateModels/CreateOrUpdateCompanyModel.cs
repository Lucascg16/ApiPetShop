namespace ApiPetShop.Domain;

public record CreateOrUpdateCompanyModel(int Id, string Name, AddressModel Address, string PhoneNumber, string InstagramAddress);
