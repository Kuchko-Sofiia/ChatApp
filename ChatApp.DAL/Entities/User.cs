#nullable disable
using Microsoft.AspNetCore.Identity;

namespace ChatApp.DAL.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<ChatMember> ChatMembers { get; set; }
        public ICollection<Avatar> Avatars { get; set; }
    }
}
