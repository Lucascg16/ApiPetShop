using ApiPetShop.Domain;
using ApiPetShop.Infra;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController(ITokenService tokenService, IUserServices userService, ICryptoService cryptoService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] LoginModel login)
        {
            try
            {
                var userDatabase = await userService.GetUserByEmail(login.Email);

                if (userDatabase.Id == 0) return NotFound(new{message = "Email ou senha inválidos"});
                if (login.Password != cryptoService.Decrypt(userDatabase.Password))
                    return Unauthorized(new{message = "Email ou senha inválidos"});
                
                tokenService.RevokeToken(userDatabase.Id);

                var (refreshToken, refreshKey) = tokenService.GenerateRefreshToken();//gera as duas variaveis para o token
                await tokenService.SaveRefreshToken(userDatabase.Id, refreshToken, refreshKey);// salva no banco de dados ligando ao usuário que foi logado
                
                return Ok(new
                {
                    token = tokenService.GenerateToken(userDatabase),//o jwt so e passado no retorno da api, será usado para acessar api pelo front
                    refreshToken,
                    refreshKey
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new{message = ex.Message});
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel request)
        {
            try
            {
                var user = await userService.GetUserById(request.UserId);
            
                var userToken = await tokenService.GetRefreshToken(user.Id);
                if (userToken.Id == 0 || userToken.Key != request.RefreshKey) return Unauthorized();
                if (userToken.Expiration < DateTime.UtcNow) return Unauthorized();

                tokenService.RevokeToken(user.Id);
                
                var newJwt = tokenService.GenerateToken(user);
                var (refreshToken, refreshKey) = tokenService.GenerateRefreshToken(); 
                await tokenService.SaveRefreshToken(user.Id, refreshToken, refreshKey);
                        
                return Ok(new
                {
                    token = newJwt,
                    refreshToken,
                    refreshKey
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpPost("revoke")]
        public IActionResult RevokeToken(int userId)
        {
            try
            {
                tokenService.RevokeToken(userId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel nUser)
        {
            try
            {
                var userVerify = await userService.GetUserByEmail(nUser.Email);
                if (userVerify.Id != 0) return Unauthorized("Email já cadastrado");

                await userService.CreateUser(nUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}