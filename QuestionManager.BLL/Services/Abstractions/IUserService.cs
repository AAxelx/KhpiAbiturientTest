using QuestionManager.BLL.Models.Responses.Abstractions;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface IUserService
    {
        IServiceResponse GetByEmailAsync(string email);
        IServiceResponse AddResultAsync(string email, int score);
    }
}
