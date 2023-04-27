using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DAL.Data.Configurations
{
    public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
    {
        public void Configure(EntityTypeBuilder<ChatMember> builder)
        {
            builder.ToTable("ChatMembers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ChatId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne(x => x.Chat)
                   .WithMany(x => x.ChatMembers)
                   .HasForeignKey(x => x.ChatId)
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(x => x.ChatMembers)
                   .HasForeignKey(x => x.UserId)
                   .IsRequired();
        }
    }
}
