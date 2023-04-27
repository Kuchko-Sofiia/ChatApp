namespace ChatApp.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int ChatId { get; set; }
        public string FromUserId { get; set; } = null!;
        public string ToUserId { get; set; } = null!;
        public string ForwardedFromUserId { get; set; } = null!;
        public DateTime SentTime { get; set; }
        public int Status { get; set; }
    }
}
