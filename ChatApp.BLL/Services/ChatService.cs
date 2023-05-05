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

        public async Task<Chat> GetChatById(int id)
        {
            var chatRepository = _unitOfWork.GetRepository<IChatRepository>();
            return await chatRepository.GetById(id);
        }

        public async Task<PaginatedData<ChatInfo>> GetPaginatedChatsAsync(PaginatedDataStateDTO<ChatSortProperty> tableState)
        {
            var chatInfoRepository = _unitOfWork.GetRepository<IChatInfoRepository>();
            var chats = chatInfoRepository.GetAll();

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

            return await PaginatedData<ChatInfo>.GetPaginatedDataAsync(
                source: chats,
                pageIndex: tableState.PageIndex,
                pageSize: tableState.PageSize);
        }

        public async Task<PaginatedData<ChatInfo>> GetPaginatedChatsByUserIdAsync(PaginatedDataStateDTO<ChatSortProperty> tableState, string userId)
        {
            var chatInfoRepository = _unitOfWork.GetRepository<IChatInfoRepository>();
            var chatsToJoin = chatInfoRepository.GetAll();

            var chatMemberRepository = _unitOfWork.GetRepository<IChatMemberRepository>();
            var chatMembers = chatMemberRepository.GetAll();

            var chats = from chat in chatsToJoin
                        join chatMember in chatMembers
                        on chat.ChatId equals chatMember.ChatId
                        where chatMember.UserId == userId
                        select new ChatInfo
                        {
                            ChatId = chat.ChatId,
                            Name = chat.Name,
                            Description = chat.Description,
                            MembersCount = chat.MembersCount,
                            Avatar = chat.Avatar
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

            return await PaginatedData<ChatInfo>.GetPaginatedDataAsync(
                source: chats,
                pageIndex: tableState.PageIndex,
                pageSize: tableState.PageSize);
        }
    }
}
