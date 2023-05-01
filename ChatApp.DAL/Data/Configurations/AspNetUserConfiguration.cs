using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ChatApp.DAL.Entities;

namespace ChatApp.DAL.Data.Configurations
{
    public class AspNetUserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AspNetUsers");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(256);
            builder.Property(u => u.LastName).HasMaxLength(256);
            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);
            builder.Property(u => u.PasswordHash).HasMaxLength(2000);
            builder.Property(u => u.SecurityStamp).HasMaxLength(2000);
            builder.Property(u => u.ConcurrencyStamp).HasMaxLength(2000);
            builder.Property(u => u.PhoneNumber).HasMaxLength(50);
            builder.Property(u => u.AccessToken).HasMaxLength(2000);
            builder.Property(u => u.RefreshToken).HasMaxLength(2000);
            //builder.Property(u => u.LastTimeActive).HasColumnType("datetime");
            builder.Property(u => u.DateOfBirth).HasColumnType("datetime2");
            builder.Property(u => u.RefreshTokenExpiryTime).HasColumnType("datetime2");

            builder.HasMany(u => u.ChatMembers)
                   .WithOne(c => c.User)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Avatars)
                   .WithOne(a => a.User)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
