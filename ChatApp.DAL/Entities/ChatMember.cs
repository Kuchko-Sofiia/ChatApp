#nullable disable

namespace ChatApp.DAL.Entities
{
    public class ChatMember
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string UserId { get; set; }
        public Chat Chat { get; set; }
        public User User { get; set; }
    }
}