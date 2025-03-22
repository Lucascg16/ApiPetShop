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
                await SendVetNotification();
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task SendVetNotification()
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DbConnectionContext>();
        var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();
        
        var now = DateTime.UtcNow;
        var clients = await db.VetServices.Where(x => x.ScheduledDate.DayOfYear == now.AddDays(1).DayOfYear && x.ScheduledDate.Year == now.Year).ToListAsync();

        foreach (var customer in clients)
        {
            if (!string.IsNullOrEmpty(customer.Email))
                await emailService.SendRememberEmail(customer.Email, customer.Name, customer.ScheduledDate, null!);//Todo: numero da empresa, precisa adicionar

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