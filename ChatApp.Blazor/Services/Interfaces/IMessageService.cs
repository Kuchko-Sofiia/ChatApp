using ChatApp.DTO;

namespace ChatApp.Blazor.Services.Interfaces
{
    public interface IMessageService
    {
        public Task CreateNewMessage(MessageDTO messageDto);
        public Task<List<MessageDTO>> GetAllMessagesAsync(int chatId);
        public Task<PaginatedDataDTO<MessageDTO>> GetMessagesAsync(PaginatedDataStateDTO<MessageSortProperty> tableState);
    }
}
