namespace ChatApp.DAL.Entities
{
    public class Message
    {
        public int? Id { get; set; }
        public string? MessageText { get; set; }
        public int ChatId { get; set; }
        public string? FromUserId { get; set; }
        public string? ToUserId { get; set; }
        public string? ForwardedFromUserId { get; set; }
        public DateTime? SentTime { get; set; }
        public int? MessageStatus { get; set; }

        public Chat? Chat { get; set; }
        public User? FromUser { get; set; }
        public User? ToUser { get; set; }
        public User? ForwardedFromUser { get; set; }
    }
}
