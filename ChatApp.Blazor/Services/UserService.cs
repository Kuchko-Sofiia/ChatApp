using ChatApp.DTO;
using ChatApp.Blazor.Services.Interfaces;
using System.Text.Json;

namespace ChatApp.Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly IWrappedHttpClient _httpClient;

        public UserService(IWrappedHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginatedDataDTO<UserInfoDTO>> GetUsersAsync(TableStateData<UserInfoSortProperty> tableState)
        {
            var response = await _httpClient.PostAsJsonAsync("user/getallusers", tableState);

            response.EnsureSuccessStatusCode();

            var paginatedData = 
                await response.Content.ReadFromJsonAsync<PaginatedDataDTO<UserInfoDTO>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) 
                ?? new PaginatedDataDTO<UserInfoDTO>();

            return paginatedData;
        }
    }
}
