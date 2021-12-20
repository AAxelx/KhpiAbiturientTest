using QuestionManager.BLL.Models.Responses;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface IUserService
    {
        GetByEmailResponse GetByEmail(string email);
    }
}
