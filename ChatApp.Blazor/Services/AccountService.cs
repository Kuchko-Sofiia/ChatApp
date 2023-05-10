using ChatApp.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using ChatApp.Blazor.Helpers;
using ChatApp.DTO.Authentication;

namespace ChatApp.Blazor.Services
{
    public class AccountService : IAccountService
    {
        private readonly IWrappedHttpClient _httpClient;
        private readonly ICustomLocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AccountService(IWrappedHttpClient httpClient, ICustomLocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task SignIn(SignInDTO signInDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/signin", signInDto);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
            }
        }

        public async Task Login(LoginDTO loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

                if(authResponse != null)
                    await _localStorageService.SetJwtTokenInfoAsync(authResponse);

                await ((CustomAuthenticationStateProvider)_authenticationStateProvider).AuthenticationStateChanged();
            }
        }

        public async Task ChangePassword(ChangePasswordDTO changePasswordDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/changepassword", changePasswordDto);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                await _localStorageService.SetJwtTokenAsync(token);
            }
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveJwtTokenInfoAsync();
            await ((CustomAuthenticationStateProvider)_authenticationStateProvider).AuthenticationStateChanged();
        }
    }
}
