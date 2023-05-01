using ChatApp.DAL.Entities;

namespace ChatApp.DAL.Repositories.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        public IQueryable<Message> GetAllByChatId(int id);
    }
}
