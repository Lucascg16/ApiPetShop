
using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public class ScheduleService(IPetRepository petRepository, IVetRepository vetRepository) : IScheduleService
    {
        private readonly IPetRepository _petRepository = petRepository;
        private readonly IVetRepository _vetRepository = vetRepository;

        public async Task<List<string>> GetPetServiceAvailable(DateTime date)
        {
            var AvailebleTimes = PopulateAvailableTimes(date);
            var ScheduledDates = await _petRepository.GetScheduledTime(date);

            foreach (var ScheduledDate in ScheduledDates)
            {
                AvailebleTimes.Remove($"{ScheduledDate.Hour.ToString("D2")}:{ScheduledDate.Minute.ToString("D2")}");
            }

            return AvailebleTimes;
        }

        public async Task<List<string>> GetVetServiceAvailable(DateTime date)
        {
            var AvailebleTimes = PopulateAvailableTimes(date);
            var ScheduledDates = await _vetRepository.GetScheduledTime(date);

            foreach (var ScheduledDate in ScheduledDates)
            {
                AvailebleTimes.Remove($"{ScheduledDate.Hour.ToString("D2")}:{ScheduledDate.Minute.ToString("D2")}");
            }

            return AvailebleTimes;
        }

        private static List<string> PopulateAvailableTimes(DateTime date)
        {
            List<string> listTimes = [];
            int hora = 8;

            if (date.DayOfYear == DateTime.Now.DayOfYear)
                hora = DateTime.Now.Hour + 1;
            if (date.DayOfYear < DateTime.Now.DayOfYear)
                return [];

            for (; hora < 17; hora++)
            {
                listTimes.Add($"{hora:D2}:00");
                if (hora < 17)
                {
                    listTimes.Add($"{hora:D2}:30");
                }
            }

            return listTimes;
        }
    }
}
