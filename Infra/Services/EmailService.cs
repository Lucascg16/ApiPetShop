using System.Net;
using System.Net.Mail;
using ApiPetShop.Domain;
using Microsoft.Extensions.Options;

namespace ApiPetShop.Infra;

public class EmailService : IEmailService
{
    private readonly EmailModel _emailModel;

    public EmailService(IOptions<EmailModel> emailModel)
    {
        _emailModel = emailModel.Value;
    }
    
    public async Task SendPasswordEmailAsync(string userEmail, string subject, string msg, string token)
    {
        if (string.IsNullOrEmpty(userEmail)) throw new("O email do usuário não pode ser vazio");
        
        var callBackUrl = $"{_emailModel.WebAddress}/senha/redefinirSenha?Token={token}";//Todo: Verificar no site como que vai ficar a url de callback

        var email = new MailMessage();
        email.From = new MailAddress(_emailModel.EmailSender, _emailModel.Sender);
        email.To.Add(userEmail);
        email.Subject = subject;
        email.Body = EmailTemplates.RedefinirSenha.Replace("{callBackUrl}", callBackUrl);
        email.IsBodyHtml = true;
        
        await EmailConfig().SendMailAsync(email);
    }
    
    private SmtpClient EmailConfig()
    {
        return new SmtpClient()
        {
            Host = _emailModel.ServerAddress,
            Port = _emailModel.ServerPort,
            EnableSsl = _emailModel.UseSsl,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(
                _emailModel.EmailSender,
                _emailModel.Password
            )
        };    
    }
    public class EmailTemplates{
        public static string RedefinirSenha = "Conforme solicitado segue o link para redefinição de senha: <a href='{callBackUrl}'>Clique aqui</a>. <br> Caso não tenha solicitado a troca de senha por favor desconsidere o e-mail";
    }
}