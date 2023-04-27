using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.DAL.Data.Configurations
{
    public class ChatInfoConfiguration : IEntityTypeConfiguration<ChatInfo>
    {
        public void Configure(EntityTypeBuilder<ChatInfo> builder)
        {
            builder.ToView("ChatInfo");
            builder.HasNoKey();
        }
    }
}
