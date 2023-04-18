using ChatApp.Blazor.Data;

namespace ChatApp.Blazor.Services.Interfaces
{
    public interface ICustomLocalStorageService
    {
        public Task SetJwtTokenInfoAsync(AuthResponseDTO authResponse);
        public Task<string> GetJwtTokenAsync();
        public Task SetJwtTokenAsync(string token);
        public Task RemoveJwtTokenInfoAsync();
    }
}
