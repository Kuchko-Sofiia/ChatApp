
namespace ChatApp.DAL.Entities
{
    public partial class Chat
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Descriptoin { get; set; }

        public virtual ICollection<ChatMembers> ChatMembers { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}