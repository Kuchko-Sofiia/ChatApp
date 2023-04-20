using System.Text.Json;

namespace ChatApp.Blazor.Services.Interfaces
{
    public interface IWrappedHttpClient
    {
        public Task<HttpResponseMessage> GetAsync(string requestUri);
        public Task<HttpResponseMessage> GetAsync(Uri? requestUri);
        public Task<HttpResponseMessage> PostAsync(string? requestUri, HttpContent? content);
        public Task<HttpResponseMessage> PostAsync(Uri? requestUri, HttpContent? content);
        public Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default);
        public Task<HttpResponseMessage> PostAsJsonAsync<TValue>(Uri? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default);
        public Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string? requestUri, TValue value, CancellationToken cancellationToken);
        public Task<HttpResponseMessage> PostAsJsonAsync<TValue>(Uri? requestUri, TValue value, CancellationToken cancellationToken);
        public Task<HttpResponseMessage> PutAsync(string? requestUri, HttpContent? content);
        public Task<HttpResponseMessage> PutAsync(Uri? requestUri, HttpContent? content);
        public Task<HttpResponseMessage> PatchAsync(string? requestUri, HttpContent? content);
        public Task<HttpResponseMessage> PatchAsync(Uri? requestUri, HttpContent? content);
        public Task<HttpResponseMessage> DeleteAsync(string? requestUri);
        public Task<HttpResponseMessage> DeleteAsync(Uri? requestUri);
    }
}
