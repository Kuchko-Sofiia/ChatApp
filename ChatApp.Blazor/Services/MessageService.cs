using ChatApp.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using ChatApp.DTO;
using System.Text.Json;
using System.Net;

namespace ChatApp.Blazor.Services
{
    public class MessageService : IMessageService
    {
        private readonly IWrappedHttpClient _httpClient;

        public MessageService(IWrappedHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateNewMessage(MessageDTO messageDto)
        {
            await _httpClient.PostAsJsonAsync("api/message/create", messageDto);
        }

        public async Task<List<MessageDTO>> GetAllMessagesAsync(int chatId)
        { 
            var queryStringParam = new Dictionary<string, string?>()
            {
                ["chatId"] = chatId.ToString()
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/message/getall", queryStringParam));
            var data = await response.Content.ReadFromJsonAsync<List<MessageDTO>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return data;
        }
        public async Task<PaginatedDataDTO<MessageDTO>> GetMessagesAsync(PaginatedDataStateDTO<MessageSortProperty> tableState)
        {
            var response = await _httpClient.PostAsJsonAsync("api/message/getpaginated", tableState);

            response.EnsureSuccessStatusCode();

            var paginatedData =
                await response.Content.ReadFromJsonAsync<PaginatedDataDTO<MessageDTO>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new PaginatedDataDTO<MessageDTO>();

            return paginatedData;
        }
    }
}
