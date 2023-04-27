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

            builder.Property(m => m.Text)
                .HasMaxLength(int.MaxValue);

            builder.Property(m => m.SentTime)
                .HasColumnType("datetime");

            builder.Property(m => m.MessageStatus)
                .HasColumnType("int");

            builder.HasOne(m => m.Chat)
                .WithMany()
                .HasForeignKey(m => m.ChatId);

            builder.HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
