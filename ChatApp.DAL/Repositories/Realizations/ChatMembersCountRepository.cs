using ChatApp.DAL.Data;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;

namespace ChatApp.DAL.Repositories.Realizations
{
    public class ChatMembersCountRepository : Repository<ChatMembersCount>, IChatMembersCountRepository
    {
        public ChatMembersCountRepository(ChatAppDbContext context) : base(context) { }
    }
}
