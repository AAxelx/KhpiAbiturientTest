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
        public async Task<IActionResult> GetAll()
        {
            var response = await _questionService.GetAllAsync();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRequest request)
        {
            var response = await _questionService.AddAsync(request.Question, request.Answear, request.Complexity, request.SecondOption, request.ThirdOption);

            return Ok(response);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid questionId)
        {
            var response = await _questionService.DeleteAsync(questionId);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveResult(CalculatePointsRequest request)
        {
            var response = await _questionService.SaveResultAsync(request.Questions, request.Email);

            return Ok(response);
        }
    }
}
