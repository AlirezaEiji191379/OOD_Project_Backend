using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace OOD_Project_Backend.Core.DataAccess.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        IQueryable<T> GetAll(bool trackChanges);
        Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
