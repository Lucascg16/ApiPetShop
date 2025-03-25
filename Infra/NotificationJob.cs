using ApiPetShop.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Infra;

public class NotificationJob(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;
            if (now is { Hour: 8, Minute: 0 })
            {
                await SendNotification();
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task SendNotification()
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DbConnectionContext>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        
        await SendToVetCustumers(db, emailService);
        await SendToPetCustumers(db, emailService);
    }

    private static async Task SendToVetCustumers(DbConnectionContext db, IEmailService emailService)
    {
        var now = DateTime.UtcNow;
        var clients = await db.VetServices.AsNoTracking().Where(x => x.ScheduledDate.DayOfYear == now.AddDays(1).DayOfYear && x.ScheduledDate.Year == now.Year).ToListAsync();
        //Essa consulta ignora o primeiro dia do ano
        foreach (var customer in clients)
        {
            if (!string.IsNullOrEmpty(customer.Email))
                await emailService.SendRememberEmail(customer.Email, customer.Name, customer.ScheduledDate, TypeServiceEnum.Vet);

            if (string.IsNullOrEmpty(customer.PhoneNumber)) 
                continue;
            
            //Todo: Desenvolver o envio de sms

            if (customer.IsWhatsApp)
            {
                //Todo: Desenvolver o envio de wpp
            }
        }   
    }

    private static async Task SendToPetCustumers(DbConnectionContext db, IEmailService emailService)
    {
        var now = DateTime.UtcNow;
        var clients = await db.PetServices.AsNoTracking().Where(x => x.ScheduledDate.DayOfYear == now.AddDays(1).DayOfYear && x.ScheduledDate.Year == now.Year).ToListAsync();

        foreach (var customer in clients)
        {
            if (!string.IsNullOrEmpty(customer.Email))
                await emailService.SendRememberEmail(customer.Email, customer.Name, customer.ScheduledDate, TypeServiceEnum.Pet);

            if (string.IsNullOrEmpty(customer.PhoneNumber)) 
                continue;
            
            //Todo: Desenvolver o envio de sms

            if (customer.IsWhatsApp)
            {
                //Todo: Desenvolver o envio de wpp
            }
        }
    }
}