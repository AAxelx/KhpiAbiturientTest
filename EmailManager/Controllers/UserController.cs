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
        private readonly IMessageService _messageService;

        public UserController(
            ILetterService letterService, 
            IMessageService messageService)
        {
            _letterService = letterService;
            _messageService = messageService;
        }

        [HttpPost]
        public async Task SendEmail()
        {
            var email = "dkaz.photogrphy@gmail.com";
            var messageSettings = _messageService.CreateMessageDetails(email, 15);
            var result = await _letterService.SendAsync(messageSettings);
        }
    }
}
