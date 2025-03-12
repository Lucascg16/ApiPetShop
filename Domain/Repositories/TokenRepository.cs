using ApiPetShop.Infra;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Domain;

public class TokenRepository(DbConnectionContext db) : ITokenRepository
{
    public async Task<TokenModel> GetRefreshToken(int userId) => await db.Tokens.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId) ?? new();

    public async Task CreateRefreshToken(TokenModel token)
    {
        await db.Tokens.AddAsync(token);
        await db.SaveChangesAsync();
    }

    public void RovokeToken(TokenModel userToken)
    {
        db.Tokens.Remove(userToken);
        db.SaveChanges();
    }
}