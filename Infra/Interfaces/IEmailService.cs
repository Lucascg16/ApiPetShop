namespace ApiPetShop.Infra;

public interface IEmailService
{
    Task SendPasswordEmailAsync (string userEmail, string subject, string token);
    Task SendRememberEmail(string email, string name, DateTime scheduleTime, string phone);
}