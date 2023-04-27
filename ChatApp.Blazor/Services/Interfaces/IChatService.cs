using ChatApp.DTO;

namespace ChatApp.Blazor.Services.Interfaces
{
    public interface IChatService
    {
        public Task CreateNewChat(ChatDTO chatDto);
        public Task<PaginatedDataDTO<ChatDTO>> GetChatsAsync(TableStateData<ChatSortProperty> tableState);
    }
}
