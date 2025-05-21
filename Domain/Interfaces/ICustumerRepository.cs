namespace ApiPetShop.Domain
{
    public interface ICustumerRepository
    {
        Task Create(string email);
        Task<CustumerModel> Get(string email);
        Task<List<CustumerModel>> GetAll();
    }
}