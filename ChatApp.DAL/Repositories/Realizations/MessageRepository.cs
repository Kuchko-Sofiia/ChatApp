using ChatApp.DAL.Data;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DAL.Repositories.Realizations
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ChatAppDbContext context) : base(context) { }

        public async Task<IQueryable<Message>> GetAllByChatId(int id)
        {
            return _dbSet.Where(m => m.ChatId == id).Include(m => m.Sender).Include(m => m.Chat).OrderByDescending(m => m.SentTime);
        }
    }
}
