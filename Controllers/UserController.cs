﻿using ApiPetShop.Domain;
using ApiPetShop.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Create(CreateUserModel user)
        {
            try
            {
                await _userServices.CreateUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public IActionResult Update(UpdateUserModel user)
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

        [HttpPatch("password")]
        public async Task<IActionResult> UpdatePass(UpdatePasswordModel update)
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
