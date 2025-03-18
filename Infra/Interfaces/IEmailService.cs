namespace ApiPetShop.Infra;

public interface IEmailService
{
    Task SendPasswordEmailAsync (string userEmail, string subject, string token);
}