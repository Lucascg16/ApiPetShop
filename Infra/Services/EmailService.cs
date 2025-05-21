using System.Net;
using System.Net.Mail;
using ApiPetShop.Domain;
using ApiPetShop.Domain.Enum;
using Microsoft.Extensions.Options;

namespace ApiPetShop.Infra;

public class EmailService(IOptions<EmailModel> emailModel, ICompanyService company, ICustumerService custumerService) : IEmailService
{
    private readonly EmailModel _emailModel = emailModel.Value;

    public async Task SendRememberEmail(string email, string name, DateTime scheduleTime, TypeServiceEnum type)
    {
        var companie = await company.GetCompany();
        var locale = $"{companie.Address.Street}, {companie.Address.Neighborhood}, {companie.Address.City}, {companie.Address.State},  {companie.Address.ZipCode}";

        string body = EmailTemplates.Remember.Replace("{date}", $"{scheduleTime.ToLocalTime()}")
                                            .Replace("{locale}", locale)
                                            .Replace("{custumer}", name)
                                            .Replace("{contact}", companie.PhoneNumber ?? "Não informado")
                                            .Replace("{type}", type == TypeServiceEnum.Vet ? "a vacina" : "o banho/tosa")
                                            .Replace("{petDoc}", type == TypeServiceEnum.Vet ? "<h2>Não esqueça de levar a carteirinha de vacinação! Se precisar reagendar, é só avisar.</h2>" : "");

        await EmailConfig().SendMailAsync(PrepareEmailToSend(email, EmailTemplates.RememberTitle, body));//Todo: Analisar fomra de alterar o email conforme o tipo de servico, pensando em criar um servicemodel e fazer a galera herdar dele 
    }

    public async Task SendPasswordEmailAsync(string userEmail, string subject, string token)
    {
        if (string.IsNullOrEmpty(userEmail)) throw new("O email do usuário não pode ser vazio");

        var callBackUrl = $"{_emailModel.WebAddress}/reset?Token={token}";
        string body = EmailTemplates.RedefinirSenha.Replace("{callBackUrl}", callBackUrl);

        await EmailConfig().SendMailAsync(PrepareEmailToSend(userEmail, subject, body));
    }

    public async Task SendCustomEmail(string subject, string msg)
    {
        if (string.IsNullOrEmpty(subject)) subject = "Comunicação!!!";
        var custumers = await custumerService.GetAll();

        foreach (var custumer in custumers)
        {
            await EmailConfig().SendMailAsync(PrepareEmailToSend(custumer.Email, subject, msg + EmailTemplates.UnsubSufix.Replace("urlSite", $"{_emailModel.WebAddress}")));//Todo: adicionar o caminho
            //não deve funcionar assim, a não ser que vc queira cair em uma black list.
        }
    }

    public async Task SendCreateUserEmail(string email, string subject, string msg)
    {
        if (string.IsNullOrEmpty(email)) throw new("O Email deve está preenchido");
        await EmailConfig().SendMailAsync(PrepareEmailToSend(email, subject, msg));
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
    private MailMessage PrepareEmailToSend(string destinationEmail, string subject, string bodyMessage)
    {
        var email = new MailMessage { };
        email.From = new MailAddress(_emailModel.EmailSender, _emailModel.Sender);
        email.To.Add(destinationEmail);
        email.Subject = subject;
        email.Body = bodyMessage;
        email.IsBodyHtml = true;

        return email;
    }
}
public class EmailTemplates
{
    public const string CriarConta = @"
    Confome solicitado pelo PetShop, sua conta foi criada com acesso ao dashboard 
    <br>
    Senha temporária gerada para acessar sua conta: {password}
    <br>
    ATENÇÃO: Essa é uma senha temporária, é recomendável que troque assim que possível
    <br>
    Não compartilhe sua senha com ninguem 
    ";
    public const string RedefinirSenha = "Conforme solicitado segue o link para redefinição de senha: <a href='{callBackUrl}'>Clique aqui</a>. <br> Caso não tenha solicitado a troca de senha por favor desconsidere o e-mail";
    public const string Remember = @"
    <h1>Olá {custumer}!</h1>
    <h3>Só passando para lembrar que {type} do seu pet está agendada para:</h3>
    <p>📅 Data: {date}</p>
    <p>📍 Local: {locale}</p>
    {petDoc}
    <p>📞 Contato: {contact}</p>
    <span>Até lá! 🐶🐱✨</span>
    ";
    public const string RememberTitle = "✨️Lembrete de agendamento✨";
    public const string UnsubSufix = @"
    <br><br>
    Caso não queira receber os nosso proximos emails clique <a ref='{urlSite}'>aqui</a>.
    ";
}