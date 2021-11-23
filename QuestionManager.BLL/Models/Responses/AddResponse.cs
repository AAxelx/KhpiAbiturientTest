using QuestionManager.BLL.Models.Responses.Abstractions;

namespace QuestionManager.BLL.Models.Responses
{
    public class AddResponse : IServiceResponse
    {
        public QuestionModel AddedQuestion { get; set; }
        public string Message { get; set; }
    }
}
