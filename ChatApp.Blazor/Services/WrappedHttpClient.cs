using ChatApp.Blazor.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ChatApp.Blazor.Services
{
    public class WrappedHttpClient: IWrappedHttpClient
    {
        private readonly ICustomLocalStorageService _localStorage;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public WrappedHttpClient(ICustomLocalStorageService localStorage, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _localStorage = localStorage;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            using var client = await GetHttpClientAsync();
            return await client.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> GetAsync(Uri? requestUri)
        {
            using var client = await GetHttpClientAsync();
            return await client.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PostAsync(string? requestUri, HttpContent? content)
        {
            using var client = await GetHttpClientAsync();
            return await client.PostAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> PostAsync(Uri? requestUri, HttpContent? content)
        {
            using var client = await GetHttpClientAsync();
            return await client.PostAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            using var client = await GetHttpClientAsync();
            return await client.PostAsJsonAsync(requestUri, value, options, cancellationToken);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<TValue>(Uri? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            using var client = await GetHttpClientAsync();
            return await client.PostAsJsonAsync(requestUri, value, options, cancellationToken);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string? requestUri, TValue value, CancellationToken cancellationToken)
        {
            using var client = await GetHttpClientAsync();
            return await client.PostAsJsonAsync(requestUri, value, options: null, cancellationToken);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<TValue>(Uri? requestUri, TValue value, CancellationToken cancellationToken)
        {
            using var client = await GetHttpClientAsync();
            return await client.PostAsJsonAsync(requestUri, value, options: null, cancellationToken);
        }

        public async Task<HttpResponseMessage> PutAsync(string? requestUri, HttpContent? content)
        {
            using var client = await GetHttpClientAsync();
            return await client.PutAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> PutAsync(Uri? requestUri, HttpContent? content)
        {
            using var client = await GetHttpClientAsync();
            return await client.PutAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> PatchAsync(string? requestUri, HttpContent? content)
        {
            using var client = await GetHttpClientAsync();
            return await client.PatchAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> PatchAsync(Uri? requestUri, HttpContent? content)
        {
            using var client = await GetHttpClientAsync();
            return await client.PatchAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string? requestUri)
        {
            using var client = await GetHttpClientAsync();
            return await client.DeleteAsync(requestUri);
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri? requestUri)
        {
            using var client = await GetHttpClientAsync();
            return await client.DeleteAsync(requestUri);
        }

        private async Task<HttpClient> GetHttpClientAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var accessToken = await _localStorage.GetJwtTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            httpClient.BaseAddress = new Uri(_configuration["AppBase"]);

            return httpClient;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //var httpClient = _httpClientFactory.CreateClient();
                //await _localStorage.RemoveJwtTokenInfoAsync();
            }
        }
    }
}
