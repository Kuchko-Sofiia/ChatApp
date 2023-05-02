using ChatApp.DTO;
using ChatApp.Blazor.Services.Interfaces;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ChatApp.Blazor.Services
{
    public class UserService : IUserService
    {
        private readonly IWrappedHttpClient _httpClient;

        public UserService(IWrappedHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var queryStringParam = new Dictionary<string, string?>()
            {
                ["userId"] = id
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/user/getbyid", queryStringParam));

            var data = await response.Content.ReadFromJsonAsync<UserDTO>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return data;
        }
        public async Task<PaginatedDataDTO<UserDTO>> GetUsersAsync(PaginatedDataStateDTO<UserInfoSortProperty> tableState)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/getpaginatedusers", tableState);

            response.EnsureSuccessStatusCode();

            var paginatedData = 
                await response.Content.ReadFromJsonAsync<PaginatedDataDTO<UserDTO>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) 
                ?? new PaginatedDataDTO<UserDTO>();

            return paginatedData;
        }
        public async Task EditUserAsync(UserDTO userToEdit)
        {
            var json = JsonSerializer.Serialize(userToEdit);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync("api/user/edit", content);
        }

        public async Task AddAvatarAsync(AvatarDTO avatar)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/avatar/add", avatar);

            response.EnsureSuccessStatusCode();
        }
        public async Task RemoveAvatarAsync(int avatarId)
        {
            var response = await _httpClient.DeleteAsync($"api/user/avatar/remove/{avatarId}");

            response.EnsureSuccessStatusCode();
        }
    }
}
