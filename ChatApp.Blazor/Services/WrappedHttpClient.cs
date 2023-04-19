using Blazored.LocalStorage;
using ChatApp.Blazor.Services.Interfaces;
using System.Net.Http.Headers;

namespace ChatApp.Blazor.Services
{
    public class WrappedHttpClient: IWrappedHttpClient
    {
        private readonly ICustomLocalStorageService _localStorage;
        private readonly IHttpClientFactory _httpClientFactory;

        public WrappedHttpClient(ICustomLocalStorageService localStorage, IHttpClientFactory httpClientFactory)
        {
            _localStorage = localStorage;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            using var client = await GetHttpClientAsync();

            var response = await client.GetAsync(url);

            return response;
        }

        private async Task<HttpClient> GetHttpClientAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var accessToken = await _localStorage.GetJwtTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return httpClient;
        }
    }
}
