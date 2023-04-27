using ChatApp.DAL.Entities;

namespace ChatApp.DAL.Repositories.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        public Task<IQueryable<Message>> GetAllByChatId(int id);
    }
}
