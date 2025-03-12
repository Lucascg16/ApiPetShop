using ApiPetShop.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiPetShop.Infra
{
    public class TokenService(IConfiguration config, ITokenRepository tokenRepository) : ITokenService
    {
        private readonly IConfiguration _config = config;

        public async Task<TokenModel> GetRefreshToken(int userId) => await tokenRepository.GetRefreshToken(userId);
        
        public string GenerateToken(UserModel user, int expires = 2, bool isInvalid = false)
        {
            var secret = _config.GetSection("ApiSettings")["Secret"];
            var key = Encoding.ASCII.GetBytes(string.IsNullOrEmpty(secret) ? throw new("O secret é necessário para gerar o token") : secret);
            SigningCredentials signingCredentials = null!;

            if (!isInvalid)
            {
                signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            }

            var tokenOptions = new JwtSecurityToken(
                issuer: _config.GetSection("ApiSettings")["Issuer"] ?? "",
                audience: _config.GetSection("ApiSettings")["Audience"] ?? "",
                claims: [
                    new Claim("id", user.Id.ToString()),
                    new Claim("role", user.Role.ToString())
                ],
                expires: DateTime.Now.AddHours(expires),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task SaveRefreshToken(int userId, string refreshToken, string refreshKey)
        {
            var tokenModel = new TokenModel(userId, refreshToken, refreshKey, DateTime.UtcNow.AddDays(2));
            await tokenRepository.CreateRefreshToken(tokenModel);
        }

        public void RevokeToken(TokenModel userToken)
        {
            tokenRepository.RovokeToken(userToken);
        }

        public (string refreshToken, string refreshKey) GenerateRefreshToken()
        {
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var refreshKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            
            return (refreshToken, refreshKey);
        }
    }
}
