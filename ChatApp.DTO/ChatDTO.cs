namespace ChatApp.DTO
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int MembersCount { get; set; }
        public List<string>? MembersId { get; set; } = new List<string>();
    }
    public enum ChatSortProperty
    {
        None,
        Name,
        Description,
        MembersCount
    }
}
