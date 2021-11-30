using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestionManager.DAL.DataAccess.Contracts
{
    public interface IDbRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>()
           where T : class, IEntity;
        Task<T> GetByIdAsync<T>(Guid id)
            where T : class, IEntity;
        Task<T> AddAsync<T>(T newEntity)
            where T : class, IEntity;
        Task<bool> GetByEmail<T>(string email)
            where T : class, IEntity;
        Task<bool> UpdateAsync<T>(T entity)
            where T : class, IEntity;
        Task<bool> RemoveAsync<T>(Guid id)
            where T : class, IEntity;
    }
}
