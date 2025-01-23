using ApiPetShop.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/vacines")]
    public class VacineController(IVacineRepository repository) : ControllerBase
    {
        private readonly IVacineRepository _vacineRepository = repository;

        [HttpGet]
        public async Task<IActionResult> GetVacines()
        {
            return Ok(await _vacineRepository.GetAll());
        }
    }
}
