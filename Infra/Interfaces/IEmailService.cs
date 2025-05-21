using ApiPetShop.Domain.Enum;

namespace ApiPetShop.Infra;

public interface IEmailService
{
    Task SendPasswordEmailAsync (string userEmail, string subject, string token);
    Task SendRememberEmail(string email, string name, DateTime scheduleTime, TypeServiceEnum type);
    Task SendCustomEmail(string subject, string msg);
}