using System.Collections.Generic;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface IGoogleSheetsService
    {
        bool AddUser(string[] data);
        bool CheckByEmail(string email);
        IList<IList<object>> GetAllQuestions();
        string GetMessage();
    }
}
