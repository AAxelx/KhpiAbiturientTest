using QuestionManager.BLL.Models;
using QuestionManager.BLL.Models.Responses.Abstractions;
using System.Collections.Generic;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface IQuestionService
    {
        IServiceResponse GetAll();
        IServiceResponse SaveResult(List<AnswearModel> questions, string email);
        int CalculatePoints(List<AnswearModel> questions);
    }
}
