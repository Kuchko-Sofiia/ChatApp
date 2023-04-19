using ChatApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.BLL.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int id);
        public Task<List<User>> GetAllUsersAsync();
    }
}