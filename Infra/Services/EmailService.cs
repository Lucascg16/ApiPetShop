using System.Net;
using System.Net.Mail;
using ApiPetShop.Domain;
using Microsoft.Extensions.Options;

namespace ApiPetShop.Infra;

public class EmailService(IOptions<EmailModel> emailModel) : IEmailService
{
    private readonly EmailModel emailModel = emailModel.Value;

    public async Task SendRememberEmail(string email, string name, DateTime scheduleTime, string phone)
    {
        string locale = "Vitória - ES, Jardim Camburi, Rua ali no canto direito";
        string body = EmailTemplates.Remember.Replace("{date}", $"{scheduleTime.ToLocalTime()}").Replace("{locale}", locale).Replace("{custumer}", name).Replace("{contact}", phone ?? "Não informado");
        await EmailConfig().SendMailAsync(PrepareEmailToSend(email, EmailTemplates.RememberTitle, body));//Todo: Analisar fomra de alterar o email conforme o tipo de servico, pensando em criar um servicemodel e fazer a galera herdar dele 
    }

    public async Task SendPasswordEmailAsync(string userEmail, string subject, string token)
    {
        if (string.IsNullOrEmpty(userEmail)) throw new("O email do usuário não pode ser vazio");

        var callBackUrl = $"{emailModel.WebAddress}/reset?Token={token}";
        string body = EmailTemplates.RedefinirSenha.Replace("{callBackUrl}", callBackUrl);

        await EmailConfig().SendMailAsync(PrepareEmailToSend(userEmail, subject, body));
    }

    private SmtpClient EmailConfig()
    {
        return new SmtpClient()
        {
            Host = emailModel.ServerAddress,
            Port = emailModel.ServerPort,
            EnableSsl = emailModel.UseSsl,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(
                emailModel.EmailSender,
                emailModel.Password
            )
        };
    }
    public MailMessage PrepareEmailToSend(string destinationEmail, string subject, string bodyMessage)
    {
        var email = new MailMessage { };
        email.From = new MailAddress(emailModel.EmailSender, emailModel.Sender);
        email.To.Add(destinationEmail);
        email.Subject = subject;
        email.Body = bodyMessage;
        email.IsBodyHtml = true;

        return email;
    }
}
public class EmailTemplates
{
    public static string RedefinirSenha = "Conforme solicitado segue o link para redefinição de senha: <a href='{callBackUrl}'>Clique aqui</a>. <br> Caso não tenha solicitado a troca de senha por favor desconsidere o e-mail";
    public static string Remember = @"
    <h1>Olá {custumer}!</h1>
    <h3>Só passando para lembrar que a vacinação do seu pet está agendada para:</h3>
    <p>📅 Data: {date}</p>
    <p>📍 Local: {locale}</p>
    <h2>Não esqueça de levar a carteirinha de vacinação! Se precisar reagendar, é só avisar.</h2>
    <p>📞 Contato: {contact}</p>
    <span>Até lá! 🐶🐱✨</span>
    ";
    public static string RememberTitle = "✨️Lembrete de agendamento✨";
}