using ChatApp.BLL.Models;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.UnitOfWork;
using ChatApp.DTO;

namespace ChatApp.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateMessage(Message newMessage)
        {
            var messageRepository = _unitOfWork.GetRepository<IMessageRepository>();
            messageRepository.Create(newMessage);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<Message>> GetAllMessagesAsync(int chatId)
        {
            var messageRepository = _unitOfWork.GetRepository<IMessageRepository>();
            var messages = await messageRepository.GetAllByChatId(chatId);
            return messages.ToList();
        }

        public async Task<PaginatedData<Message>> GetPaginatedMessagesAsync(TableStateData<MessageSortProperty> tableState)
        {
            var messageRepository = _unitOfWork.GetRepository<IMessageRepository>();
            var messages = messageRepository.GetAll();

            if (!String.IsNullOrWhiteSpace(tableState.SearchText))
            {
                string searchText = tableState.SearchText.ToLower();
                messages = messages.Where(m =>
                    m.Text.ToLower().Contains(searchText));
            }

            messages = tableState.SortProperty switch
            {
                MessageSortProperty.None => messages,
                MessageSortProperty.ChatId => (tableState.SortDirection == SortDirectionData.Ascending)
                    ? messages.OrderBy(c => c.ChatId)
                    : messages.OrderByDescending(c => c.ChatId),
                MessageSortProperty.SenderId => (tableState.SortDirection == SortDirectionData.Ascending)
                    ? messages.OrderBy(c => c.SenderId)
                    : messages.OrderByDescending(c => c.SenderId),
                MessageSortProperty.SentTime => (tableState.SortDirection == SortDirectionData.Ascending)
                    ? messages.OrderBy(c => c.SentTime)
                    : messages.OrderByDescending(c => c.SentTime),
                _ => messages
            };

            return await PaginatedData<Message>.GetPaginatedDataAsync(
                source: messages,
                pageIndex: tableState.PageIndex,
                pageSize: tableState.PageSize);
        }
    }
}
