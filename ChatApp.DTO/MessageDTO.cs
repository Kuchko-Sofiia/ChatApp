namespace ChatApp.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int ChatId { get; set; }
        public ChatDTO? Chat { get; set; }
        public string SenderId { get; set; } = null!;
        public UserInfoDTO? Sender { get; set; }
        public DateTime SentTime { get; set; }
        public int Status { get; set; }
    }
    public enum MessageSortProperty
    {
        None,
        ChatId,
        SenderId,
        SentTime
    }
}
