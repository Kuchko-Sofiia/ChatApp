namespace ChatApp.DAL.Entities
{
    public class ChatInfo
    {
        public int ChatId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int MembersCount { get; set; }
    }
}
