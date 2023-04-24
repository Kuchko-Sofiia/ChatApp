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

        public new async Task AuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public static Dictionary<string, string>? GetClaimsFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is not JwtSecurityToken jwtToken)
                return null;

            var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);

            var name = claims.GetValueOrDefault("name");
            var id = claims.GetValueOrDefault("NameIdentifier");
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(id))
            {
                claims.Add(ClaimTypes.Name, name);
                claims.Add(ClaimTypes.NameIdentifier, id);
            }

            return claims;
        }
    }
}
