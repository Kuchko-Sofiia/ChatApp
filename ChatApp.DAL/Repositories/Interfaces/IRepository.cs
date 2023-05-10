using System.Linq.Expressions;

namespace ChatApp.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<T> GetById(string id);
        public Task<T> GetById(int id);

        public IQueryable<T> GetAll();

        public IEnumerable<T> GetAllSorted(Expression<Func<T, object>> orderBy);

        public IEnumerable<T> FindAll(Expression<Func<T, bool>>? predicate = default);

       public void Create(T entity);

        public void CreateMultiple(IEnumerable<T> entities);

        public void Update(T entity);

        public void Delete(T entity);
    }
}
