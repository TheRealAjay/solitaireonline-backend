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

        public async Task AddRangeAsync(IEnumerable<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
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

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);

            if (asNoTracking)
                return await query.AsNoTracking().SingleAsync();

            return await query.SingleAsync();
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

        public async Task RemoveRangeAsync(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Entry(entity).State = EntityState.Modified;
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool asNoTracking)
        {
            if (asNoTracking)
                return _dbSet.AsNoTracking().AnyAsync(filter);

            return _dbSet.AnyAsync(filter);
        }
    }
}
