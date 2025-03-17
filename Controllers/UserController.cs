using ApiPetShop.Domain;
using ApiPetShop.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/v1/users")]
    public class UserController(IUserServices services) : ControllerBase
    {
        private readonly IUserServices _userServices = services;

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _userServices.GetAllUsers());
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _userServices.GetUserByIdDto(id);
                if(user.Id == 0)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserModel nUser)
        {
            try
            {
                var userVerify = await _userServices.GetUserByEmail(nUser.Email);

                if (userVerify.Id != 0) return Unauthorized(new { error = "Email já cadastrado"});

                await _userServices.CreateUser(nUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public IActionResult Update([FromBody] UpdateUserModel user)
        {
            try
            {
                _userServices.UpdateUser(user);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPatch("password")]
        public async Task<IActionResult> UpdatePass([FromBody] UpdatePasswordModel update)
        {
            try
            {
                await _userServices.UpdatePassword(update);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [AllowAnonymous]
        [HttpPatch("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            try
            {
                await _userServices.ResetPassword(model);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userServices.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
