using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Core.DataAccess.Abstractions;
using System.Linq.Expressions;

namespace OOD_Project_Backend.Core.DataAccess.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Create(T entity)
        {
            return _dbContext.Set<T>().AddAsync(entity).AsTask();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges
                ? _dbContext.Set<T>().Where(expression)
                : _dbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public IQueryable<T> GetAll(bool trackChanges)
        {
            return trackChanges
                ? _dbContext.Set<T>()
                : _dbContext.Set<T>().AsNoTracking();
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
