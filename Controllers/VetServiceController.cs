using ApiPetShop.Domain;
using ApiPetShop.Infra;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [ApiController]
    [Route("api/v1/vetservices")]
    public class VetServiceController(IVetServices services) : ControllerBase
    {
        private readonly IVetServices _services = services;

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
        public async Task<IActionResult> Create(VetServiceModel service)
        {
            await _services.CreateService(service);
            return Ok();
        }

        [HttpPatch]
        public IActionResult Update(VetServiceModel service) 
        { 
            _services.Update(service);
            return Ok();
        }
    }
}
