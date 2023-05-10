using ChatApp.DAL.Data;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;

namespace ChatApp.DAL.Repositories.Realizations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ChatAppDbContext context) : base(context) { }

        public async Task<User> GetUserWithAvatarById(string id)
        {
            return await _dbSet.Include(u => u.Avatars).SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<User> GetAllUsersWithAvatars()
        {
            return _dbSet.Include(u => u.Avatars);
        }
    }
}
