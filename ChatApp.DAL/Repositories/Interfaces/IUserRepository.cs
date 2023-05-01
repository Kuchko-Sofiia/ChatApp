using ChatApp.DAL.Entities;

namespace ChatApp.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUserWithAvatarById(string id);
        public IEnumerable<User> GetAllUsersWithAvatars();
    }
}
