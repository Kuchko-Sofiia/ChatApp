using ChatApp.DAL.Enums;

namespace ChatApp.DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public Chat Chat { get; set; }
        public User FromUser { get; set; }
        public User ToUser { get; set; }
        public User ForwardedFromUser { get; set; }
        public DateTime SentTime { get; set; }
        public MessageStatus MessageStatus { get; set; }
    }
}
