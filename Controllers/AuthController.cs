using ApiPetShop.Domain;
using ApiPetShop.Infra;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [ApiController]
    [Route("api/v1/Auth")]
    public class AuthController(ITokenService tokenService, IUserServices userService, ICryptoService cryptoService) : ControllerBase
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
                
                if (userDatabase.Id == 0) return NotFound("Email ou senha invalidos");
                if (login.Password != _cryptoService.Decrypt(userDatabase.Password)) return Unauthorized("Email ou senha invalidos");

                return Ok(_tokenService.GenerateToken(userDatabase));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel nUser)
        {
            try
            {
                var userVerify = await _userServices.GetUserByEmail(nUser.Email);
                if(userVerify.Id != 0) return Unauthorized("Email já cadastrado");

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
