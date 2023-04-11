using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.Repositories.Realizations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;

        Task<int> SaveChangesAsync();
    }
}
