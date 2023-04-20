namespace ChatApp.Blazor.Data.Authentication
{
    public class AuthResponseDTO
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
