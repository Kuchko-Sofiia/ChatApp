using ChatApp.DTO;

namespace ChatApp.Blazor.Services.Interfaces
{
    public interface IChatService
    {
        public Task CreateNewChat(ChatDTO chatDto);
        public Task<ChatDTO> GetChatAsync(int chatId);
        public Task<PaginatedDataDTO<ChatDTO>> GetChatsAsync(PaginatedDataStateDTO<ChatSortProperty> tableState);
    }
}
