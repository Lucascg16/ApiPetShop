using ApiPetShop.Infra;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain;

public class CompanyRepository(DbConnectionContext db, IMapper mapper) : ICompanyRepository
{
    public async Task CreateCompany(CompanyModel company)
    {
        await db.Companie.AddAsync(company);
        await db.SaveChangesAsync();
    }

    public void UpdateCompany(CompanyModel company)
    {
        db.Companie.Update(company);
        db.SaveChanges();
    }

    public async Task<CompanyDto> GetCompany()
    {
        return mapper.Map<CompanyDto>(await db.Companie.AsNoTracking().Include(x => x.Address).FirstOrDefaultAsync()) ?? new();
    }

    public async Task<CompanyModel> GetCompanyModel()
    {
        return await db.Companie.AsNoTracking().Include(x => x.Address).FirstOrDefaultAsync() ?? new();
    }
}