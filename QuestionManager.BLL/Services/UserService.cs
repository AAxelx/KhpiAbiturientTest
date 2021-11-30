using QuestionManager.BLL.Models.Responses;
using QuestionManager.BLL.Models.Responses.Abstractions;
using QuestionManager.BLL.Services.Abstractions;
using QuestionManager.DAL.DataAccess.Contracts;
using QuestionManager.DAL.DataAccess.Implementations.Entities;
using System;
using System.Threading.Tasks;

namespace QuestionManager.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IDbRepository _dbRepository;

        public UserService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public async Task<IServiceResponse> GetByEmailAsync(string email)
        {
            var created = await _dbRepository.GetByEmail<UserEntity>(email);

            if (!created)
            {
                return new GetByEmailResponse() { Created = created };
            }

            return new GetByEmailResponse() { Message = "The user is already taking part." };
        }

        public async Task<IServiceResponse> AddResultAsync(string email, int score)
        {
            var response = await GetByEmailAsync(email);

            if(response.Message == null)
            {
                var result = _dbRepository.AddAsync(new UserEntity()
                {
                    Id = Guid.NewGuid(),
                    Email = email,
                    Score = score
                });

                if(result != null)
                {
                    return new AddResultResponse() { Success = true };
                }

                response.Message = response.Message + "The user wasn't added to database";
            }

            return new AddResultResponse() { Message = response.Message };
        }
    }
}
