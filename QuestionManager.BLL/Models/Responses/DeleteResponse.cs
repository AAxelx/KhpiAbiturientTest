using QuestionManager.BLL.Models.Responses.Abstractions;

namespace QuestionManager.BLL.Models.Responses
{
    public class DeleteResponse : IServiceResponse
    {
        public bool IsRemoved { get; set; }
        public string Message { get; set; }
    }
}
