using QuestionManager.BLL.Models.Responses.Abstractions;

namespace QuestionManager.BLL.Models.Responses
{
    public class RemoveResponse : IServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
