using ApiPetShop.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiPetShop.Infra
{
    public class TokenService(IConfiguration config) : ITokenService
    {
        private readonly IConfiguration _config = config;

        public string GenerateToken(UserModel user, int expires = 2)
        {
            var secret = _config.GetSection("ApiSettings")["Secret"];
            var key = Encoding.ASCII.GetBytes(string.IsNullOrEmpty(secret) ? throw new("O secret é necessário para gerar o token") : secret);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

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
    }
}
