using ApiPetShop.Domain.Models;
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

    [HttpPost("custom")]
    public async Task<IActionResult> SendRememberEmail(CustomEmailModel email)
    {
        try
        {
            await emailService.SendCustomEmail(email.Subject, email.Body);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}