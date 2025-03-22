using ApiPetShop.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [ApiController]
    [Route("api/v1/availableTimes")]
    public class AvailableTimesController(IScheduleService scheduleService) : ControllerBase
    {
        [HttpGet("pet")]
        public async Task<IActionResult> GetPetAvailable(DateTime date)
        {
            try
            {
                return Ok(await scheduleService.GetPetServiceAvailable(date));
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
                return Ok(await scheduleService.GetVetServiceAvailable(date));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
