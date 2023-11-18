using Microsoft.EntityFrameworkCore;
using Solitaire.DataAccess.Context;
using Solitaire.DataAccess.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Solitaire.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<T?> FindAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);

            if (asNoTracking)
                return await query.AsNoTracking().FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = false)
        {
            IQueryable<T> query = _dbSet;

            if (asNoTracking)
                return await query.AsNoTracking().ToListAsync();

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool asNoTracking)
        {
            if (asNoTracking)
                return _dbSet.AsNoTracking().AnyAsync(filter);

            return _dbSet.AnyAsync(filter);
        }
    }
}
