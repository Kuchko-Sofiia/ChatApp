using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ChatApp.DAL.Data.Configurations
{
    public class ChatMembersCountConfiguration : IEntityTypeConfiguration<ChatMembersCount>
    {
        public void Configure(EntityTypeBuilder<ChatMembersCount> builder)
        {
            builder.ToTable("ChatUserCount");
            builder.HasNoKey();
        }
    }
}
