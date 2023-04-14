using ChatApp.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ChatApp.DAL.Data
{
    public class ChatAppDbContext : IdentityDbContext<User>
    {
        //public DbSet<Message> Messages { get; set; }
        //public DbSet<Chat> Chats { get; set; }
        //public DbSet<ChatMembers> ChatMembers { get; set; }
        //public DbSet<ContactRequest> ContactRequests { get; set; }
        //public DbSet<Contacts> Contacts { get; set; }
        //public DbSet<BlockedUsers> BlockedUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options) : base(options) { }
    }
}
