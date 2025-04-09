namespace ApiPetShop.Domain;

public record CreateOrUpdateCompanyModel(int Id, string Name, string ContactEmail, AddressModel Address, string PhoneNumber, string InstagramAddress);
