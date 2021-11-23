using QuestionManager.BLL.Models.Responses.Abstractions;
using System.Collections.Generic;

namespace QuestionManager.BLL.Models.Responses
{
    public class GetAllResponse : IServiceResponse
    {
        public List<QuestionModel> EasyQuestions { get; set; }
        public List<QuestionModel> MediumQuestions { get; set; }
        public List<QuestionModel> HardQuestions { get; set; }
        public string Message { get; set; }
    }
}
