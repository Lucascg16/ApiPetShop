using ApiPetShop.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    [ApiController]
    [Route("api/v1/vacines")]
    public class VacineController(IVacineRepository repository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetVacines()
        {
            return Ok(await repository.GetAll());
        }
    }
}
