using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DAL.Data.Configurations
{
    public class AvatarConfiguration : IEntityTypeConfiguration<Avatar>
    {
        public void Configure(EntityTypeBuilder<Avatar> builder)
        {
            builder.ToTable("Avatars");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.FileName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(a => a.ContentType)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.Content)
                .IsRequired();

            builder.HasOne(a => a.User)
                .WithMany(u => u.Avatars)
                .HasForeignKey(a => a.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(a => a.Chat)
                .WithMany(c => c.Avatars)
                .HasForeignKey(a => a.ChatId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
