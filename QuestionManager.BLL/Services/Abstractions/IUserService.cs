using QuestionManager.BLL.Models.Responses.Abstractions;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface IUserService
    {
        IServiceResponse GetByEmail(string email);
        IServiceResponse AddResult(string email, int score);
    }
}
