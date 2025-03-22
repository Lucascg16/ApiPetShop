
using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public class ScheduleService(IPetRepository petRepository, IVetRepository vetRepository) : IScheduleService
    {
        public async Task<List<string>> GetPetServiceAvailable(DateTime date)
        {
            var availableTimes = PopulateAvailableTimes(date);
            var scheduledDates = await petRepository.GetScheduledTime(date);

            foreach (var scheduledDate in scheduledDates)
            {
                availableTimes.Remove($"{scheduledDate.Hour:D2}:{scheduledDate.Minute:D2}");
            }

            return availableTimes;
        }

        public async Task<List<string>> GetVetServiceAvailable(DateTime date)
        {
            var availableTimes = PopulateAvailableTimes(date);
            var scheduledDates = await vetRepository.GetScheduledTime(date);

            foreach (var scheduledDate in scheduledDates)
            {
                availableTimes.Remove($"{scheduledDate.Hour:D2}:{scheduledDate.Minute:D2}");
            }

            return availableTimes;
        }

        private static List<string> PopulateAvailableTimes(DateTime date)
        {
            List<string> listTimes = [];
            var hora = 8;

            if (date.DayOfYear == DateTime.Now.DayOfYear)
                hora = DateTime.Now.Hour + 1;
            if (date.DayOfYear < DateTime.Now.DayOfYear && date.Year <= DateTime.Now.Year)
                return [];

            for (; hora < 17; hora++)
            {
                if (hora == 12) continue;
                listTimes.Add($"{hora:D2}:00");
                listTimes.Add($"{hora:D2}:30");
            }

            return listTimes;
        }
    }
}
