namespace ApiPetShop.Infra
{
    public interface IScheduleService
    {
        Task<List<string>> GetPetServiceAvailable(DateTime date);
        Task<List<string>> GetVetServiceAvailable(DateTime date);
    }
}
