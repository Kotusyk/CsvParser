using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Model;
using App.Services;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        IRequestService _requestService;
        public UsersController(IRequestService requestServise)
        {
            _requestService = requestServise;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()

        {
            return Ok(await _requestService.GetUsers());
        }

        [HttpGet("GetUserById/{Id}")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            return Ok(await _requestService.GetUserById(Id));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] User task)
        {
            var result = await _requestService.CreateUser(task);
            if (result.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok(task);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User task)
        {
            await _requestService.UpdateUser(task);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _requestService?.DeleteUser(id);
            return Ok(id);
        }
    }
}
