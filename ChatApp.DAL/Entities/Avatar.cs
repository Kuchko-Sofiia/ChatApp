namespace ChatApp.DAL.Entities
{
    public class Avatar : IdentifiableEntity
    {
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public byte[] Content { get; set; } = null!;
        public string? UserId { get; set; }
        public int? ChatId { get; set; }

        public User? User { get; set; }
        public Chat? Chat { get; set; }
    }
}
