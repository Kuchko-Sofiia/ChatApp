namespace ChatApp.DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int ChatId { get; set; }
        public string? SenderId { get; set; }
        public DateTime SentTime { get; set; }
        public int MessageStatus { get; set; }

        public Chat? Chat { get; set; }
        public User? Sender { get; set; }
    }
}
