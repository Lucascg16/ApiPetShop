namespace ApiPetShop.Domain;

public interface ICompanyRepository
{
    Task CreateCompany (CompanyModel company);
    void UpdateCompany(CompanyModel company);
    Task<CompanyDto> GetCompany();
    Task<CompanyModel> GetCompanyModel();
}