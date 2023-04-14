using ChatApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.DAL.Repositories.Realizations
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
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

        public IEnumerable<T> GetChunk(int pageNumber, int pageSize, Expression<Func<T, object>>? orderBy = null, bool ascending = true, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbSet.AsQueryable();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<T> GetChunk(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, Expression<Func<T, object>>? orderBy = null, bool ascending = true, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbSet.Where(predicate);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
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
