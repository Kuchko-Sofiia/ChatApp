using ChatApp.Blazor.Services.Interfaces;
using Blazored.LocalStorage;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using ChatApp.Blazor.Helpers;
using Microsoft.Extensions.Configuration;
using System.Security.Principal;
using ChatApp.Blazor.Data.Authentication;

namespace ChatApp.Blazor.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ICustomLocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AccountService(HttpClient httpClient, ICustomLocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["AppBase"];
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task SignIn(SignInDTO signInDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}account/signin", signInDto);
            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if(authResponse != null)
                    await _localStorageService.SetJwtTokenInfoAsync(authResponse);
            }
            else
            {
                // TODO: Handle error response
            }
        }

        public async Task Login(LoginDTO loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}account/login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if(authResponse != null)
                    await _localStorageService.SetJwtTokenInfoAsync(authResponse);

                await ((CustomAuthenticationStateProvider)_authenticationStateProvider).AuthenticationStateChanged();
            }
            else
            {
                // TODO: Handle error response
            }
        }

        public async Task ChangePassword(ChangePasswordDTO changePasswordDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}account/changepassword", changePasswordDto);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                await _localStorageService.SetJwtTokenAsync(token);
            }
            else
            {
                // TODO: Handle error response
            }
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveJwtTokenInfoAsync();
            await ((CustomAuthenticationStateProvider)_authenticationStateProvider).AuthenticationStateChanged();
        }
    }
}
