namespace ApiPetShop.Domain;

public record EmailModel
{
    public string Sender { get; set; } = "";
    public string EmailSender { get; set; } = "";
    public string Password { get; set; } = "";
    public string ServerAddress { get; set; } = "";
    public int ServerPort { get; set; }
    public bool UseSsl { get; set; } = false;
    public string WebAddress { get; set; } = "";
};