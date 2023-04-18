using Blazored.LocalStorage;
using ChatApp.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ChatApp.Blazor.Services
{
    public class TestPageService : ITestPageService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public TestPageService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<string> GetMessageAsync()
        {
            var accessToken = await _localStorageService.GetItemAsync<string>("token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://localhost:7158/account/test");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var responseMessage = JsonSerializer.Deserialize<JsonElement>(jsonResponse).GetProperty("responseMessage").GetString();

                return responseMessage;
            }

            return "You're unauthorized";
        }
    }
}
