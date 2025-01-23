using ApiPetShop.Domain;
using ApiPetShop.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/petservice")]
    public class PetServiceController(IPetServices services) : ControllerBase
    {
        private readonly IPetServices _services = services;

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _services.GetAllPetServices());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _services.GetServiceById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PetServiceModel service)
        {
            await _services.CreateService(service);
            return Ok();
        }

        [HttpPatch]
        public IActionResult Update(PetServiceModel service) 
        {
            _services.Update(service);
            return Ok();
        }
    }
}
