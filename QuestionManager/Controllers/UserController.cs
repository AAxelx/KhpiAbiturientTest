using Microsoft.AspNetCore.Mvc;
using QuestionManager.BLL.Services.Abstractions;

namespace QuestionManager.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult GetByEmail(string email)
        {
            var response = _userService.GetByEmail(email);

            return Ok(response);
        }
    }
}
