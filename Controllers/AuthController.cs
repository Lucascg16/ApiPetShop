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

        [HttpPost("generatePassToken")]
        public async Task<IActionResult> GeneratePassToken(string email)
        {
            try
            {
                var userDataBase = await _userServices.GetUserByEmail(email);
                if (userDataBase.Id == 0) return Unauthorized();

                return Ok(_tokenService.GenerateToken(userDataBase, 3, true));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
