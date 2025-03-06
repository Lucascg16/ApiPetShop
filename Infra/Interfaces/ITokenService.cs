using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface ITokenService
    {
        string GenerateToken(UserModel user, int expires = 4, bool isInvalid = false);
    }
}
