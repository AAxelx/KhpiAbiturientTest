using Microsoft.EntityFrameworkCore;
using QuestionManager.DAL.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestionManager.DAL.DataAccess.Implementations
{
    public class DbRepository : IDbRepository
    {
        private readonly AppDbContext _context;

        public DbRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
            where T : class, IEntity
        {
            var result = await _context.Set<T>().AsQueryable().ToListAsync();

            if (result == null)
            {
                return null;
            }

            return result;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
            where T : class, IEntity
        {
            var entity = await _context.Set<T>().AsQueryable().FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<T> AddAsync<T>(T newEntity)
            where T : class, IEntity
        {
            var result = await _context.Set<T>().AddAsync(newEntity);

            if (result == null)
            {
                return null;
            }

            await SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> RemoveAsync<T>(Guid id)
            where T : class, IEntity
        {
            var result = _context.Set<T>().AsQueryable().FirstOrDefaultAsync(u => u.Id == id);

            if (result == null)
            {
                return false;
            }

            _context.Remove(result);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync<T>(T entity)
            where T : class, IEntity
        {
            var result = _context.Set<T>().AsQueryable().FirstOrDefaultAsync(u => u.Id == entity.Id);

            if (result == null)
            {
                return false;
            }

            _context.Set<T>().Update(entity);
            await SaveChangesAsync();
            return true;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
