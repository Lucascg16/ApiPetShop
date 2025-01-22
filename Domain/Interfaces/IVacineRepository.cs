namespace ApiPetShop.Domain
{
    public interface IVacineRepository
    {
        Task<List<VacineModel>> GetAll();
    }
}
