using ApiPetShop.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [ApiController]
    [Route("api/v1/availableTimes")]
    public class AvailableTimesController(IScheduleService service) : ControllerBase
    {
        private readonly IScheduleService _scheduleService = service;

        [HttpGet("pet")]
        public async Task<IActionResult> GetPetAvailable(DateTime date)
        {
            try
            {
                return Ok(await _scheduleService.GetPetServiceAvailable(date));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("vet")]
        public async Task<IActionResult> GetVetAvailable(DateTime date)
        {
            try
            {
                return Ok(await _scheduleService.GetVetServiceAvailable(date));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
