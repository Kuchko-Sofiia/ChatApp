using ChatApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<T> GetById(string id);

        IQueryable<T> GetAll();

        IEnumerable<T> GetAllSorted(Expression<Func<T, object>> orderBy);

        IEnumerable<T> FindAll(Expression<Func<T, bool>>? predicate = default);

        IEnumerable<T> FindAllWithRelated(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<T> GetChunk(
            int pageNumber, 
            int pageSize, 
            Expression<Func<T, object>>? orderBy = null, 
            bool ascending = true,
            params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<T> GetChunk(
            Expression<Func<T, bool>> predicate,
            int pageNumber,
            int pageSize,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            params Expression<Func<T, object>>[] includeProperties);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Attach(T entity);
    }
}
