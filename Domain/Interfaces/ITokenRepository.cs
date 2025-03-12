namespace ApiPetShop.Domain;

public interface ITokenRepository
{
    Task<TokenModel> GetRefreshToken(int userId);

    Task CreateRefreshToken(TokenModel token);
    void RovokeToken(TokenModel userToken);
}