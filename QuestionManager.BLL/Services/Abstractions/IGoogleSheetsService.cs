using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface IGoogleSheetsService
    {
        bool AddUser(string[] data);
    }
}
