using ApiPetShop.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/crypt")]
    public class CryptoController(ICryptoService service) : ControllerBase
    {
        private readonly ICryptoService _cryptoService = service;

        [HttpGet("crypt")]
        public IActionResult Crypt(string input)
        {
            try
            {
                return Ok(_cryptoService.Encrypt(input));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("decrypt")]
        public IActionResult Decrypt(string input) 
        {
            try
            {
                return Ok(_cryptoService.Decrypt(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
