using ApiPetShop.Domain;
using ApiPetShop.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/company")]
public class CompanyController(ICompanyService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCompany()
    {
        try
        {
            return Ok(await service.GetCompany());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostCompany(CreateOrUpdateCompanyModel nCompany)
    {
        try
        {
            await service.Create(nCompany);
            return Ok();
        }
        catch  (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> PatchCompany(CreateOrUpdateCompanyModel nCompany)
    {
        try
        {
            await service.Update(nCompany);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }   
    }
}