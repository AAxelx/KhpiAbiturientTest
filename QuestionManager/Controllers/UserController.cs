﻿using Microsoft.AspNetCore.Mvc;
using QuestionManager.BLL.Models.Requests;
using QuestionManager.BLL.Services.Abstractions;
using System.Threading.Tasks;

namespace QuestionManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var response = await _userService.GetByEmailAsync(email);

            if(response.Message == null)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(RemoveUserRequest request)
        {
            await _userService.RemoveAsync(request.Password, request.Id);

            return NoContent();
        }
    }
}
