using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public class CustumerService(ICustumerRepository repository) : ICustumerService
    {
        public async Task Create(string email)
        {
            await repository.Create(email);
        }

        public async Task<CustumerModel> Get(string email)
        {
            return await repository.Get(email);
        }

        public async Task<List<CustumerModel>> GetAll()
        {
            return await repository.GetAll();
        }
    }
}