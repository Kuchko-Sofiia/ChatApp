using ChatApp.Blazor.Services.Interfaces;
using ChatApp.DTO;
using ChatApp.DTO.Authentication;
using System.Text.Json;

namespace ChatApp.Blazor.Services
{
    public class ChatService : IChatService
    {
        private readonly IWrappedHttpClient _httpClient;

        public ChatService(IWrappedHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateNewChat(ChatDTO chatDto)
        {
            var response = await _httpClient.PostAsJsonAsync("chat/create", chatDto);

        }

        public async Task<PaginatedDataDTO<ChatDTO>> GetChatsAsync(TableStateData<ChatSortProperty> tableState)
        {
            var response = await _httpClient.PostAsJsonAsync("chat/getall", tableState);

            response.EnsureSuccessStatusCode();

            var paginatedData =
                await response.Content.ReadFromJsonAsync<PaginatedDataDTO<ChatDTO>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new PaginatedDataDTO<ChatDTO>();

            return paginatedData;
        }
    }
}
