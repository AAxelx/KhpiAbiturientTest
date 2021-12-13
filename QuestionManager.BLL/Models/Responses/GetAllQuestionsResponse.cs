using QuestionManager.BLL.Models.Responses.Abstractions;
using System.Collections.Generic;

namespace QuestionManager.BLL.Models.Responses
{
    public class GetAllQuestionsResponse : IServiceResponse
    {
        public List<QuestionModel> Questions { get; set; }
        public string Message { get; set; }
    }
}
