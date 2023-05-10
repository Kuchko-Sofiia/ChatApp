#nullable disable

namespace ChatApp.DAL.Entities
{
    public class ChatMember : IdentifiableEntity
    {
        public int ChatId { get; set; }
        public string UserId { get; set; }
        public Chat Chat { get; set; }
        public User User { get; set; }
    }
}