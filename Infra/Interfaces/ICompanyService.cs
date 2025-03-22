using ApiPetShop.Domain;

namespace ApiPetShop.Infra;

public interface ICompanyService
{
    Task Create(CreateOrUpdateCompanyModel nCompany);
    Task Update(CreateOrUpdateCompanyModel nCompany);
    Task<CompanyDto>  GetCompany();
}