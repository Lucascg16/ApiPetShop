using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface ICustumerService
    {
        Task Create(string email);
        Task<CustumerModel> Get(string email);
        Task<List<CustumerModel>> GetAll();
    }
}