using ChatApp.DAL.Entities;

namespace ChatApp.DAL.Repositories.Interfaces
{
    public interface IChatMemberRepository : IRepository<ChatMember>
    {
        public IQueryable<Chat> GetUsersChats(string id);
    }
}
