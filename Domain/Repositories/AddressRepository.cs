using ApiPetShop.Infra;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain;

public class AddressRepository(DbConnectionContext db) : IAddressRepository
{
    public async Task<int> Create(AddressModel address)
    {
        await db.Addresses.AddAsync(address);
        await db.SaveChangesAsync();

        return address.Id;
    }

    public void Update(AddressModel address)
    {
        db.Addresses.Update(address);
        db.SaveChanges();
    }

    public async Task<AddressModel> GetAddress()
    {
        return await db.Addresses.FirstOrDefaultAsync() ?? new();
    }
}