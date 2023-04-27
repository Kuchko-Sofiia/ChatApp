using ChatApp.BLL.Models;
using ChatApp.DAL.Entities;
using ChatApp.DTO;

namespace ChatApp.BLL.Services.Interfaces
{
    public interface IChatService
    {
        public Task CreateChat(Chat newChat, IEnumerable<string> newChatUsers);
        public Task<PaginatedData<Chat>> GetPaginatedChatsAsync(TableStateData<ChatSortProperty> tableState);
    }
}
