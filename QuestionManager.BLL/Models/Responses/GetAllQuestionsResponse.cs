using System.Collections.Generic;

namespace QuestionManager.BLL.Models.Responses
{
    public class GetAllQuestionsResponse
    {
        public List<QuestionModel> Questions { get; set; }
    }
}
