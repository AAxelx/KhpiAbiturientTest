using Microsoft.AspNetCore.Mvc;
using QuestionManager.BLL.Models.Requests;
using QuestionManager.BLL.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace QuestionManager.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _questionService.GetAll();

            return Ok(response);
        }

        [HttpPost]
        public IActionResult SaveResult(CalculatePointsRequest request)
        {
            var response = _questionService.SaveResult(request.Questions, request.Email);

            return Ok(response);
        }
    }
}
