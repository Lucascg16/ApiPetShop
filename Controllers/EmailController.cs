using ApiPetShop.Infra;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers;

[ApiController]
[Route("api/v1/email")]
public class EmailController(IEmailService emailService, ITokenService tokenService, IUserServices userServices): ControllerBase
{
    private readonly IEmailService _emailService = emailService;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUserServices _userServices = userServices;
    
    [HttpPost]
    public async Task<IActionResult> SendPasswordEmail([FromBody] string userEmail)
    {
        try
        {
            var userDataBase = await _userServices.GetUserByEmail(userEmail);
            if (userDataBase.Id == 0) return Unauthorized("O email digitado não foi encontrado");
            
            var redefinitionToken = _tokenService.GenerateToken(userDataBase, 3, true);

            await _emailService.SendPasswordEmailAsync(userEmail, "Redefinição de senha", "", redefinitionToken);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}