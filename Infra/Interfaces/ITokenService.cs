using ApiPetShop.Domain;

namespace ApiPetShop.Infra
{
    public interface ITokenService
    {
        string GenerateToken(UserModel user, int expires = 2, bool isInvalid = false);
        Task<TokenModel> GetRefreshToken(int userId);
        Task SaveRefreshToken(int userId, string refreshToken, string refreshKey);
        (string refreshToken, string refreshKey) GenerateRefreshToken();
        void RevokeToken(int userId);
    }
}
