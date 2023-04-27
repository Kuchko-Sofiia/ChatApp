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

        public IQueryable<T> GetAll();

        public IEnumerable<T> GetAllSorted(Expression<Func<T, object>> orderBy);

        public IEnumerable<T> FindAll(Expression<Func<T, bool>>? predicate = default);

        public IEnumerable<T> FindAllWithRelated(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        public void Create(T entity);

        public void CreateMultiple(IEnumerable<T> entities);

        public void Update(T entity);

        public void Delete(T entity);

        public void Attach(T entity);
    }
}
