using ApiPetShop.Domain;
using ApiPetShop.Infra;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController(ITokenService tokenService, IUserServices userService, ICryptoService cryptoService)
        : ControllerBase
    {
        private readonly IUserServices _userServices = userService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly ICryptoService _cryptoService = cryptoService;

        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] LoginModel login)
        {
            try
            {
                var userDatabase = await _userServices.GetUserByEmail(login.Email);

                if (userDatabase.Id == 0) return NotFound("Email ou senha inválidos");
                if (login.Password != _cryptoService.Decrypt(userDatabase.Password))
                    return Unauthorized("Email ou senha inválidos");
                
                _tokenService.RevokeToken(userDatabase.Id);

                var (refreshToken, refreshKey) = _tokenService.GenerateRefreshToken();//gera as duas variaveis para o token
                await _tokenService.SaveRefreshToken(userDatabase.Id, refreshToken, refreshKey);// salva no banco de dados ligando ao usuário que foi logado
                
                return Ok(new
                {
                    token = _tokenService.GenerateToken(userDatabase),//o jwt so e passado no retorno da api, será usado para acessar api pelo front
                    refreshToken,
                    refreshKey
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel request)
        {
            try
            {
                var user = await _userServices.GetUserById(request.UserId);
            
                var userToken = await _tokenService.GetRefreshToken(user.Id);
                if (userToken.Id == 0 || userToken.Key != request.RefreshKey) return Unauthorized();
                if (userToken.Expiration < DateTime.UtcNow) return Unauthorized();

                _tokenService.RevokeToken(user.Id);
                
                var newJwt = _tokenService.GenerateToken(user);
                var (refreshToken, refreshKey) = _tokenService.GenerateRefreshToken(); 
                await _tokenService.SaveRefreshToken(user.Id, refreshToken, refreshKey);
                        
                return Ok(new
                {
                    token = newJwt,
                    refreshToken,
                    refreshKey
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("revoke")]
        public IActionResult RevokeToken(int userId)
        {
            try
            {
                _tokenService.RevokeToken(userId);
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
                var userVerify = await _userServices.GetUserByEmail(nUser.Email);
                if (userVerify.Id != 0) return Unauthorized("Email já cadastrado");

                await _userServices.CreateUser(nUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}