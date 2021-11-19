using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.BL.Abstractions;

namespace Email.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILetterService _letterService;

        public UserController(
            ILetterService letterService)
        {
            _letterService = letterService;
        }

        [HttpPost]
        public async Task SendEmail()
        {
            var email = "dkaz.photogrphy@gmail.com";
            await _letterService.Send(email);
        }
    }
}
