using ApiPetShop.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/crypt")]
    public class CryptoController(ICryptoService cryptoService) : ControllerBase
    {
        [HttpGet("crypt")]
        public IActionResult Crypt(string input)
        {
            try
            {
                return Ok(cryptoService.Encrypt(input));
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
                return Ok(cryptoService.Decrypt(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
