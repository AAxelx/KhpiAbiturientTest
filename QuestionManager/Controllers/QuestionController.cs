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

            if(response.Message == null)
            {
                return Ok(response);
            }

            return NotFound(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRequest request)
        {
            var response = await _questionService.AddAsync(request.Question, request.Answear, request.Complexity, request.SecondOption, request.ThirdOption);

            if(response.Message == null)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid questionId)
        {
            var response = await _questionService.DeleteAsync(questionId);

            if(response.Message == null)
            {
                return Ok(response);
            }

            return NotFound(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CalculatePoints(CalculatePointsRequest request)
        {
            var response = await _questionService.CalculatePointsAsync(request.Questions, request.Email);

            if (response.Message == null)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
    }
}
