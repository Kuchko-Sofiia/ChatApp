using ChatApp.Blazor.Data;
using ChatApp.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
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

        public async Task<List<UserInfoDTO>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}user/getallusers");

            response.EnsureSuccessStatusCode();

            var userInfoDtos = 
                await response.Content.ReadFromJsonAsync<List<UserInfoDTO>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) 
                ?? new List<UserInfoDTO>();

            return userInfoDtos;
        }
    }
}
