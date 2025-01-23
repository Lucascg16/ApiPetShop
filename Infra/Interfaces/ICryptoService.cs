namespace ApiPetShop.Infra
{
    public interface ICryptoService
    {
        string Encrypt(string input);
        string Decrypt(string input);
    }
}
