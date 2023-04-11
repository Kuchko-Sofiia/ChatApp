using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChatApp.DAL.Data
{
    public class ChatAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
