using ChatApp.DAL.Data;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;

namespace ChatApp.DAL.Repositories.Realizations
{
    public class AvatarRepository : Repository<Avatar>, IAvatarRepository
    {
        public AvatarRepository(ChatAppDbContext context) : base(context) { }
    }
}
