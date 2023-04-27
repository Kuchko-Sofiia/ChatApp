namespace ChatApp.DAL.Entities
{
    public class ChatMembersCount
    {
        public int ChatId { get; set; }
        public string ChatName { get; set; } = null!;
        public int MembersCount { get; set; }
    }
}
