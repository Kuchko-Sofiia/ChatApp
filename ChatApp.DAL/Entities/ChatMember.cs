#nullable disable

namespace ChatApp.DAL.Entities
{
    public class ChatMember
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string UserId { get; set; }
        public virtual Chat Chat { get; set; }
        public virtual User User { get; set; }
    }
}