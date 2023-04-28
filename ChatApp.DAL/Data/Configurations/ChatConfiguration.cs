using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.DAL.Data.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chats");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(int.MaxValue);
            builder.Ignore(c => c.MembersCount);

            builder.HasMany(x => x.ChatMembers)
                   .WithOne(x => x.Chat)
                   .HasForeignKey(x => x.ChatId)
                   .IsRequired();

            builder.HasMany(u => u.Avatars)
                   .WithOne(a => a.Chat)
                   .HasForeignKey(a => a.ChatId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
