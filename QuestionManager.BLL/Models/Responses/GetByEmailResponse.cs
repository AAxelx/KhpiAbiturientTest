using QuestionManager.BLL.Models.Responses.Abstractions;

namespace QuestionManager.BLL.Models.Responses
{
    public class GetByEmailResponse : IServiceResponse
    {
        public bool Created { get; set; }
        public string Message { get; set; }
    }
}
