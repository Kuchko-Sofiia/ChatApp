using Blazored.LocalStorage;
using ChatApp.Blazor.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ChatApp.Blazor.Services
{
    public class TestPageService : ITestPageService
    {
        private readonly IWrappedHttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public TestPageService(IWrappedHttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<string> GetMessageAsync()
        {
            var response = await _httpClient.GetAsync("api/account/test");

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
