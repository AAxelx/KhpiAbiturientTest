using EmailManager.BL.Abstractions;
using EmailManager.DAL.DataAccess.Contracts;
using EmailManager.DAL.DataAccess.Implementations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.BL.Services
{
    public class UserService
    {
        public readonly IDbRepository _repository;
        public readonly ILetterService _letterService;

        public UserService(
            IDbRepository repository,
            ILetterService letterService)
        {
            _repository = repository;
            _letterService = letterService;
        }

        public async Task<bool> AddResultAsync(string email, int score)
        {
            var user = new UserEntity() { Id = new Guid(), Email = email, Score = score };
            var result = await _repository.AddAsync<UserEntity>(user);
            if(result != null)
            {
                var sendingResult = await _letterService.Send(result.Email, result.Score);
                if (sendingResult == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
