#nullable disable
using Microsoft.AspNetCore.Identity;


namespace ChatApp.DAL.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public DateTime LastTimeActive { get; set; }
        //public Role Role { get; set; }
        //public ICollection<Chat> Chats { get; set; }
        //public ICollection<User> Contacts { get; set; }
        //public ICollection<User> BlockedUsers { get; set; }
    }
}
