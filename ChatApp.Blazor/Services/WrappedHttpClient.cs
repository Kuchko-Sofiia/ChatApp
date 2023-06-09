﻿using ChatApp.Blazor.Services.Interfaces;
using ChatApp.DTO.Authentication;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ChatApp.Blazor.Services
{
    public class WrappedHttpClient : IWrappedHttpClient
    {
        private readonly ICustomLocalStorageService _localStorage;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly NavigationManager _navigationManager;

        public WrappedHttpClient(ICustomLocalStorageService localStorage, IHttpClientFactory httpClientFactory, NavigationManager navigationManager, IConfiguration configuration)
        {
            _localStorage = localStorage;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _navigationManager = navigationManager;
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

        private async Task HandleUnauthorizedResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                if (DateTime.UtcNow != await _localStorage.GetRefreshTokenExpTimeAsync())
                {
                    await _localStorage.RemoveJwtTokenInfoAsync();
                    _navigationManager.NavigateTo("/");
                }

                var tokenDto = new TokenDTO()
                {
                    RefreshToken = await _localStorage.GetRefreshTokenAsync(),
                    AccessToken = await _localStorage.GetJwtTokenAsync()
                };

                var httpClient = _httpClientFactory.CreateClient();
                var tokenResponse = await httpClient.PostAsJsonAsync($"account/refresh-token", tokenDto);

                if (tokenResponse.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
                    await _localStorage.SetJwtTokenInfoAsync(authResponse!);
                }
                else
                {
                    await _localStorage.RemoveJwtTokenInfoAsync();
                    _navigationManager.NavigateTo("/");
                }
            }
        }

        private async Task HandleBadRequestResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                using var document = JsonDocument.Parse(responseContent);
                var errorsElement = document.RootElement.GetProperty("errors");

                var errorMessages = errorsElement.EnumerateObject()
                    .SelectMany(x => x.Value.EnumerateArray())
                    .Select(x => x.GetString())
                    .ToList();
            }
        }
    }
}
