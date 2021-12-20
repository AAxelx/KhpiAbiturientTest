using QuestionManager.BLL.Models;
using QuestionManager.BLL.Models.Responses;
using System.Collections.Generic;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface IQuestionService
    {
        GetAllQuestionsResponse GetAll();
        AddResultResponse SaveResult(List<AnswearModel> questions, string email);
        int CalculatePoints(List<AnswearModel> questions);
    }
}
