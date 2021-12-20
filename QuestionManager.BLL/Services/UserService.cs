using QuestionManager.BLL.Helpers;
using QuestionManager.BLL.Models.Responses;
using QuestionManager.BLL.Services.Abstractions;

namespace QuestionManager.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGoogleSheetsService _googleSheetsService;

        public UserService(IGoogleSheetsService googleSheetsService)
        {
            _googleSheetsService = googleSheetsService;
        }

        public GetByEmailResponse GetByEmail(string email)
        {
            var created = _googleSheetsService.CheckByEmail(email);

            if (!created)
            {
                return new GetByEmailResponse() { Created = created };
            }

            throw new AppException("The user is already taking part");
        }
    }
}
