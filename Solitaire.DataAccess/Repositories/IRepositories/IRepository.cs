using System.Linq.Expressions;

namespace Solitaire.DataAccess.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = false);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false);
        Task<T?> FindAsync(object id);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}
