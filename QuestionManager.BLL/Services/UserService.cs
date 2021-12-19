using QuestionManager.BLL.Helpers;
using QuestionManager.BLL.Models.Responses;
using QuestionManager.BLL.Models.Responses.Abstractions;
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

        public IServiceResponse GetByEmailAsync(string email)
        {
            var created = _googleSheetsService.CheckByEmail(email);

            if (!created)
            {
                return new GetByEmailResponse() { Created = created };
            }

            throw new AppException("The user is already taking part");
        }

        public IServiceResponse AddResultAsync(string email, int score)
        {
            GetByEmailAsync(email);

            var data = new string[] { email, score.ToString() }; 
            var result = _googleSheetsService.AddUser(data);

            if (result)
            {
                return new AddResultResponse() { Success = true };
            }

            throw new AppException("The user wasn't added to sheet");
        }
    }
}
