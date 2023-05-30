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
using Microsoft.AspNetCore.Cors;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text;
using AnnaMelnyk_TestTask.Services;

namespace App.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        IRequestService _requestService;
        private readonly ICSVReaderService _csvService;

        public UsersController(IRequestService requestServise, ICSVReaderService csvService)
        {
            _requestService = requestServise;
            _csvService = csvService;
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

        [HttpPost("ReadCSV")]
        public async Task<IActionResult> UploadUser([FromForm] IFormFileCollection file)
        {
            var employees = _csvService.ReadCSV<User>(file[0].OpenReadStream());

            return Ok(employees);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            var result = await _requestService.CreateUser(user);
            if (result.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            await _requestService.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _requestService?.DeleteUser(id);
            return Ok(id);
        }
    }
}
