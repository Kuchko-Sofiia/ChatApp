using ChatApp.DAL.Data;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ChatApp.DAL.Repositories.Realizations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ChatAppDbContext context) : base(context) { }
    }
}
