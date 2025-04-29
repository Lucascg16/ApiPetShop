using ApiPetShop.Domain;
using ApiPetShop.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    [ApiController]
    [Route("api/v1/vetservices")]
    public class VetServiceController(IVetServices services) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await services.GetAllPetServices());
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
                var service = await services.GetServiceByIdDto(id);
                if (service.Id == 0) return NotFound("Serviço não encontrado");

                return Ok(service);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("date")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            try
            {
                return Ok(await services.GetByDate(date));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateVetserviceModel service)
        {
            try
            {
                return Ok(await services.CreateService(service));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("relation")]
        public async Task<IActionResult> AddRelationVacine([FromBody] List<VetVacine> entities)
        {
            try
            {
                await services.ManageRelWithVacine(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Update(CreateOrUpdateVetserviceModel service)
        {
            try
            {
                await services.Update(service);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await services.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
