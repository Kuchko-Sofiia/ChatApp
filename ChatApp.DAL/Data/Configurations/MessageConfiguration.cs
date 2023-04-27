using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DAL.Data.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");

            builder.HasKey(m => m.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(m => m.MessageText)
                .HasMaxLength(int.MaxValue);

            builder.Property(m => m.SentTime)
                .HasColumnType("datetime");

            builder.Property(m => m.MessageStatus)
                .HasColumnType("int");

            builder.HasOne(m => m.Chat)
                .WithMany()
                .HasForeignKey(m => m.ChatId);

            builder.HasOne(m => m.FromUser)
                .WithMany()
                .HasForeignKey(m => m.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.ToUser)
                .WithMany()
                .HasForeignKey(m => m.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.ForwardedFromUser)
                .WithMany()
                .HasForeignKey(m => m.ForwardedFromUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
