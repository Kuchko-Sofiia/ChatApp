using ChatApp.DTO;
using ChatApp.Blazor.Services.Interfaces;
using System.Text.Json;

namespace ChatApp.Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public UserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["AppBase"];
        }

        public async Task<PaginatedDataDTO<UserInfoDTO>> GetUsersAsync(TableStateData tableState)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}user/getallusers", tableState);

            response.EnsureSuccessStatusCode();

            var paginatedData = 
                await response.Content.ReadFromJsonAsync<PaginatedDataDTO<UserInfoDTO>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) 
                ?? new PaginatedDataDTO<UserInfoDTO>();

            return paginatedData;
        }
    }
}
