using ApiPetShop.Domain;

namespace ApiPetShop.Infra;

public class CompanyService(ICompanyRepository repository, IAddressRepository addressRepository) : ICompanyService
{
    public async Task Create(CreateOrUpdateCompanyModel nCompany)
    {
        if (nCompany.Id != 0) throw new("A empresa já contem dados");
        
        var addrId = await addressRepository.Create(nCompany.Address);
        
        var company = new CompanyModel(nCompany.Name, nCompany.ContactEmail, addrId, nCompany.PhoneNumber, nCompany.InstagramAddress);
        await repository.CreateCompany(company);   
    }

    public async Task Update(CreateOrUpdateCompanyModel nCompany)
    {
        if(nCompany.Id == 0) throw new("A empresa não existe, criar será necessário");
        
        var company = await repository.GetCompanyModel();
            
        company.UpdateCompany(nCompany);
        repository.UpdateCompany(company);   
    }

    public async Task<CompanyDto> GetCompany()
    {
        return await repository.GetCompany();
    }
}