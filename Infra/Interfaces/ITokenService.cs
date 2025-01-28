using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface ITokenService
    {
        string GenerateToken(UserModel user, int expires = 2, bool isInvalid = false);
    }
}
