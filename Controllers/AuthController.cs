using ApiPetShop.Domain;
using ApiPetShop.Infra;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [ApiController]
    [Route("api/v1/Auth")]
    public class AuthController(ITokenService tokenService, IUserServices userService) : ControllerBase
    {
        private readonly IUserServices _userServices = userService;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] LoginModel login)
        {
            try
            {
                var userDatabase = await _userServices.GetUserByEmail(login.Email);
                
                if (userDatabase.Id == 0) return NotFound("Email ou senha invalidos");
                if (login.Password != userDatabase.Password) return Unauthorized("Email ou senha invalidos");

                var token = _tokenService.GenerateToken(userDatabase);
                return Ok(token);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
