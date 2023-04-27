using ChatApp.BLL.Models;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.UnitOfWork;
using ChatApp.DTO;

namespace ChatApp.BLL.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateChat(Chat newChat, IEnumerable<string> newChatUsers)
        {
            var chatRepository = _unitOfWork.GetRepository<IChatRepository>();
            chatRepository.Create(newChat);

            var chatMembersRepository = _unitOfWork.GetRepository<IChatMemberRepository>();

            await _unitOfWork.SaveChangesAsync();

            var chatMembers = newChatUsers.Select(chatMemberId => new ChatMember()
            {
                ChatId = newChat.Id,
                UserId = chatMemberId
            });

            chatMembersRepository.CreateMultiple(chatMembers);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PaginatedData<Chat>> GetPaginatedChatsAsync(TableStateData<ChatSortProperty> tableState)
        {
            var chatRepository = _unitOfWork.GetRepository<IChatRepository>();
            var chatMembersCountRepository = _unitOfWork.GetRepository<IChatMembersCountRepository>();

            var chats = chatRepository.GetAll();
            var chatMembersCount = chatMembersCountRepository.GetAll();

            chats = from chat in chats
                    join count in chatMembersCount
                    on chat.Id equals count.ChatId
                    select new Chat
                    {
                        Id = chat.Id,
                        Name = chat.Name,
                        Description = chat.Description,
                        MembersCount = count.MembersCount
                    };

            if (!String.IsNullOrWhiteSpace(tableState.SearchText))
            {
                string searchText = tableState.SearchText.ToLower();
                chats = chats.Where(u =>
                    u.Name.ToLower().Contains(searchText) ||
                    u.Description.ToLower().Contains(searchText));
            }

            chats = tableState.SortProperty switch
            {
                ChatSortProperty.None => chats,
                ChatSortProperty.Name => (tableState.SortDirection == SortDirectionData.Ascending)
                    ? chats.OrderBy(c => c.Name)
                    : chats.OrderByDescending(c => c.Name),
                ChatSortProperty.Description => (tableState.SortDirection == SortDirectionData.Ascending)
                    ? chats.OrderBy(c => c.Description)
                    : chats.OrderByDescending(c => c.Description),
                ChatSortProperty.MembersCount => (tableState.SortDirection == SortDirectionData.Ascending)
                    ? chats.OrderBy(c => c.MembersCount)
                    : chats.OrderByDescending(c => c.MembersCount),
                _ => chats
            };

            return await PaginatedData<Chat>.GetPaginatedDataAsync(
                source: chats,
                pageIndex: tableState.PageIndex,
                pageSize: tableState.PageSize);
        }
    }
}
