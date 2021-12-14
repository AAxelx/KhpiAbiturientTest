using QuestionManager.BLL.Helpers;
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

            throw new AppException("The user is already taking part");
        }

        public async Task<IServiceResponse> AddResultAsync(string email, int score)
        {
            await GetByEmailAsync(email);

            var result = await _dbRepository.AddAsync(new UserEntity()
            {
                Id = Guid.NewGuid(),
                Email = email,
                Score = score
            });

            if (result != null)
            {
                return new AddResultResponse() { Success = true };
            }

            throw new AppException("The user wasn't added to database");
        }

        public async Task<IServiceResponse> RemoveAsync(string password, Guid id)
        {
            if(password == "Helen2022Helen2022")
            {
                var result = await _dbRepository.RemoveAsync<UserEntity>(id);

                if (result)
                {
                    return new RemoveResponse() { Success = result };
                }
            }

            throw new KeyNotFoundException("User wasn't found");
        }
    }
}
