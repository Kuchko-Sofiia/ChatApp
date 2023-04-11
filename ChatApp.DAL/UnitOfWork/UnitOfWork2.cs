using ChatApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.DAL.UnitOfWork
{
    public class UnitOfWork2: IDisposable
    {
        private readonly DbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private bool _disposed;

        public UnitOfWork2(DbContext context)
        {
            _context = context;
        }

        public void RegisterRepositories()
        {
            var repositoryTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>)));

            foreach (var repositoryType in repositoryTypes)
            {
                var entityType = repositoryType.GetInterfaces().First().GetGenericArguments()[0];
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(entityType, repositoryInstance);
            }
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.TryGetValue(typeof(T), out var repository))
            {
                return (IRepository<T>)repository;
            }

            throw new ArgumentException($"Repository for entity type {typeof(T)} not found");
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
