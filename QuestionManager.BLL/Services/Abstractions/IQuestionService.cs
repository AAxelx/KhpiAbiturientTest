using QuestionManager.BLL.Models;
using QuestionManager.BLL.Models.Responses.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface IQuestionService
    {
        Task<IServiceResponse> GetAllAsync();
        Task<IServiceResponse> AddAsync(string question, string answear, int complexity, string secondOption, string thirdOption);
        Task<IServiceResponse> DeleteAsync(Guid questionId);
        Task<IServiceResponse> SaveResultAsync(List<AnswearModel> questions, string email);
        Task<int> CalculatePointsAsync(List<AnswearModel> questions);
    }
}
