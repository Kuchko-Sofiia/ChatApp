using Blazored.LocalStorage;
using ChatApp.Blazor.Data.Authentication;
using ChatApp.Blazor.Services.Interfaces;

namespace ChatApp.Blazor.Services
{
    public class CustomLocalStorageService : ICustomLocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;

        public CustomLocalStorageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task SetJwtTokenInfoAsync(AuthResponseDTO authResponse)
        {
            await _localStorageService.SetItemAsync("token", authResponse?.Token);
            await _localStorageService.SetItemAsync("refreshToken", authResponse?.RefreshToken);
            await _localStorageService.SetItemAsync("refreshTokenExpiryTime", authResponse?.RefreshTokenExpiryTime);
        }

        public async Task<string> GetJwtTokenAsync()
        {
            return await _localStorageService.GetItemAsync<string>("token");
        }

        public async Task SetJwtTokenAsync(string token)
        {
            await _localStorageService.SetItemAsync("token", token);
        }

        public async Task RemoveJwtTokenInfoAsync()
        {
            await _localStorageService.RemoveItemAsync("token");
            await _localStorageService.RemoveItemAsync("refreshToken");
            await _localStorageService.RemoveItemAsync("refreshTokenExpiryTime");
        }
    }
}