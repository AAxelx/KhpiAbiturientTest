using QuestionManager.BLL.Models.Responses.Abstractions;
using System.Threading.Tasks;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface IUserService
    {
        Task<IServiceResponse> GetByEmailAsync(string email);
        Task<IServiceResponse> AddResultAsync(string email, int score);
    }
}
