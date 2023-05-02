namespace ChatApp.DTO
{
    public class AvatarDTO
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? UserId { get; set; }
        public int? ChatId { get; set; }
    }
}
