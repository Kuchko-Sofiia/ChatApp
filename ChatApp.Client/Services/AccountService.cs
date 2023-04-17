using Microsoft.AspNetCore.Components.Authorization;
using ChatApp.Blazor.Services.Interfaces;
using ChatApp.Blazor.Data;
using Blazored.LocalStorage;

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

        public async Task SignIn(SignInModel signInModel)
        {
            var response = await _httpClient.PostAsJsonAsync("Account/signin", signInModel);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                // Store the token in local storage or do further processing
                // based on your application's requirements
            }
            else
            {
                // Handle error response
            }
        }

        public async Task Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("Account/login", loginModel);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                // Store the token in local storage or do further processing
                // based on your application's requirements
            }
            else
            {
                // Handle error response
            }
        }

        public async Task ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var response = await _httpClient.PostAsJsonAsync("Account/changepassword", changePasswordModel);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                // Store the token in local storage or do further processing
                // based on your application's requirements
            }
            else
            {
                // Handle error response
            }
        }
    }
}
