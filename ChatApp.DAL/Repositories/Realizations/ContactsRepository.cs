using ChatApp.DAL.Data;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;

namespace ChatApp.DAL.Repositories.Realizations
{
    public class ContactsRepository :Repository<Contacts>, IContactsRepository
    {
        public ContactsRepository(ChatAppDbContext context) : base(context) { }
    }
}
