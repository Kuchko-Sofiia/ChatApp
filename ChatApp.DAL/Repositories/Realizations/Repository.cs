using ChatApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChatApp.DAL.Repositories.Realizations
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private protected readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetById(string id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public IEnumerable<T> GetAllSorted(Expression<Func<T, object>> orderBy)
        {
            return _dbSet.OrderBy(orderBy).AsEnumerable();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>>? predicate = default)
        {
            return predicate != null ? _dbSet.Where(predicate).AsEnumerable() : _dbSet.AsEnumerable();
        }

        public IEnumerable<T> FindAllWithRelated(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbSet.Where(predicate);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public async void Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async void CreateMultiple(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Attach(T entity)
        {
            _dbSet.Attach(entity);
        }
    }
}
