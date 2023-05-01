#nullable disable

namespace ChatApp.DAL.Entities
{
    public partial class ContactRequest
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public int StatusId { get; set; }
        public DateTime? RequestTime { get; set; }
        public User FromUser { get; set; }
        public User ToUser { get; set; }
    }
}