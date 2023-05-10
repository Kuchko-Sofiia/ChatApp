using ChatApp.DAL.Data;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.DAL.Repositories.Realizations
{
    public class ChatMemberRepository : Repository<ChatMember>, IChatMemberRepository
    {
        public ChatMemberRepository(ChatAppDbContext context) : base(context) { }
        
        public IQueryable<Chat> GetUsersChats(string id)
        {
            return _dbSet.Where(cm => cm.UserId == id).Select(cm => cm.Chat).AsQueryable();
        }
    }
}
