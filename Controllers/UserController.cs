using ApiPetShop.Domain;
using ApiPetShop.Infra;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController(IUserServices services) : ControllerBase
    {
        private readonly IUserServices _userServices = services;

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userServices.GetAllUsers());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userServices.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel user)
        {
            await _userServices.CreateUser(user);
            return Ok();
        }

        [HttpPatch]
        public IActionResult Update(UserModel user)
        {
            _userServices.UpdateUser(user);
            return Ok();
        }
    }
}
