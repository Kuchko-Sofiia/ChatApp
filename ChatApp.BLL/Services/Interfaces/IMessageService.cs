using ChatApp.BLL.Models;
using ChatApp.DAL.Entities;
using ChatApp.DTO;

namespace ChatApp.BLL.Services.Interfaces
{
    public interface IMessageService
    {
        public Task CreateMessage(Message newMessage);
        public Task<List<Message>> GetAllMessagesAsync(int chatId);
        public Task<PaginatedData<Message>> GetPaginatedMessagesAsync(TableStateData<MessageSortProperty> tableState);
    }
}
