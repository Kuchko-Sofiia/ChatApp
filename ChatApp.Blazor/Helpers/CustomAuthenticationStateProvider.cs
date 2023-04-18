using Blazored.LocalStorage;
using ChatApp.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChatApp.Blazor.Helpers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ICustomLocalStorageService _localStorageService;

        public CustomAuthenticationStateProvider(ICustomLocalStorageService localStorage)
        {
            _localStorageService = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorageService.GetJwtTokenAsync();

                var identity = string.IsNullOrEmpty(token)
                    ? new ClaimsIdentity()
                    : new ClaimsIdentity(GetClaimsFromToken(token).Select(c => new Claim(c.Key, c.Value)), "jwt");


                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);

                return state;
            }
            catch (InvalidOperationException)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task AuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public static Dictionary<string, string>? GetClaimsFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;

            var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);

            var name = claims.GetValueOrDefault("name");
            if (!string.IsNullOrEmpty(name))
            {
                claims.Add(ClaimTypes.Name, name);
            }

            return claims;
        }
    }
}
