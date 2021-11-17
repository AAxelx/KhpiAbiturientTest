using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestionManager.DAL.DataAccess.Contracts
{
    public interface IDbRepository
    {
        Task<IEnumerable<T>> GetAll<T>()
           where T : class, IEntity;
        Task<T> GetById<T>(Guid id)
            where T : class, IEntity;
        Task<T> AddAsync<T>(T newEntity)
            where T : class, IEntity;
        Task<bool> UpdateAsync<T>(T entity)
            where T : class, IEntity;
        Task<bool> RemoveAsync<T>(Guid id)
            where T : class, IEntity;
    }
}
