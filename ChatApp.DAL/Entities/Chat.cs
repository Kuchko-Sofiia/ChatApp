namespace ChatApp.DAL.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int MembersCount { get; set; }
        public virtual ICollection<ChatMember>? ChatMembers { get; set; }
    }
}