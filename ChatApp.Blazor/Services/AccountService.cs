using Microsoft.AspNetCore.Components.Authorization;
using ChatApp.Blazor.Services.Interfaces;
using ChatApp.Blazor.Data;
using Blazored.LocalStorage;
using System.Text.Json;

namespace ChatApp.Blazor.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AccountService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task SignIn(SignInDTO signInDto)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7158/account/signin", signInDto);
            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                await _localStorageService.SetItemAsync("token", authResponse.Token);
                await _localStorageService.SetItemAsync("refreshToken", authResponse.RefreshToken);
                await _localStorageService.SetItemAsync("refreshTokenExpiryTime", authResponse.RefreshTokenExpiryTime);
            }
            else
            {
                // TODO: Handle error response
            }
        }

        public async Task Login(LoginDTO loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7158/Account/login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                await _localStorageService.SetItemAsync("token", authResponse.Token);
                await _localStorageService.SetItemAsync("refreshToken", authResponse.RefreshToken);
                await _localStorageService.SetItemAsync("refreshTokenExpiryTime", authResponse.RefreshTokenExpiryTime);
            }
            else
            {
                // TODO: Handle error response
            }
        }

        public async Task ChangePassword(ChangePasswordDTO changePasswordDto)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7158/Account/changepassword", changePasswordDto);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                await _localStorageService.SetItemAsync("token", token);
            }
            else
            {
                // TODO: Handle error response
            }
        }
    }
}
