using ChatApp.DAL.Data;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.Repositories.Realizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.DAL.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IServiceProvider _serviceProvider;
        private bool _disposed = false;

        public UnitOfWork(TContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return _serviceProvider.GetService<IRepository<T>>()
                ?? throw new ArgumentException($"Repository for entity type {typeof(T)} not found");
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
