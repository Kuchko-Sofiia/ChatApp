namespace ChatApp.DAL.Entities
{
    public class Chat : IdentifiableEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? MembersCount { get; set; }
        public ICollection<ChatMember>? ChatMembers { get; set; }
        public ICollection<Avatar>? Avatars { get; set; }
    }
}