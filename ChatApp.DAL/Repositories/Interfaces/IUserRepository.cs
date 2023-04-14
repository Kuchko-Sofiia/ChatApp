using ChatApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public User GetById(int id);

        public User TestMethod();
    }
}
