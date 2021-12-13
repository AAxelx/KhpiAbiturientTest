using QuestionManager.BLL.Models.Responses.Abstractions;
using System;
using System.Threading.Tasks;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface IUserService
    {
        Task<IServiceResponse> GetByEmailAsync(string email);
        Task<IServiceResponse> AddResultAsync(string email, int score);
        Task<IServiceResponse> RemoveAsync(string password, Guid id);
    }
}
