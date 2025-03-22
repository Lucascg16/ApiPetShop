using ApiPetShop.Infra;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers;

[ApiController]
[Route("api/v1/email")]
public class EmailController(IEmailService emailService, ITokenService tokenService, IUserServices userServices) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendPasswordEmail(string userEmail)
    {
        try
        {
            var userDataBase = await userServices.GetUserByEmail(userEmail);
            if (userDataBase.Id == 0) return Unauthorized(new { error = "O email digitado não foi encontrado" });

            var redefinitionToken = tokenService.GenerateToken(userDataBase, 3, true);

            await emailService.SendPasswordEmailAsync(userEmail, "Redefinição de senha", redefinitionToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("remember")]
    public async Task<IActionResult> SendRememberEmail(string userEmail)
    {
        try
        {
            await emailService.SendRememberEmail(userEmail, "Lucas Giacomin", DateTime.UtcNow, "(27) 99937-2743");
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}