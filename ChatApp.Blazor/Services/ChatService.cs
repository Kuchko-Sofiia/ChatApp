using ChatApp.Blazor.Services.Interfaces;
using ChatApp.DTO;
using Microsoft.AspNetCore.WebUtilities;
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
            await _httpClient.PostAsJsonAsync("chat/create", chatDto);
        }

        public async Task<ChatDTO> GetChatAsync(int chatId)
        {
            var queryStringParam = new Dictionary<string, string?>()
            {
                ["chatId"] = chatId.ToString()
            };
            var responce = await _httpClient.GetAsync(QueryHelpers.AddQueryString("chat/getbyid", queryStringParam));

            var data = await responce.Content.ReadFromJsonAsync<ChatDTO>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return data;
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
