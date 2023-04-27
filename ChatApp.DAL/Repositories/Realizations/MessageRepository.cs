using ChatApp.DAL.Data;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;

namespace ChatApp.DAL.Repositories.Realizations
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ChatAppDbContext context) : base(context) { }
    }
}
