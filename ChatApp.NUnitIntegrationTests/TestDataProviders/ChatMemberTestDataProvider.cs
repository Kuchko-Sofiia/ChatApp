using Bogus;
using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.NUnitIntegrationTests.TestDataProviders
{
    public class ChatMemberTestDataProvider : TestDataProvider<ChatMember>
    {
        protected override void ConfigureFaker(Faker<ChatMember> faker)
        {
            faker.RuleFor(cm => cm.Id, f => f.IndexFaker)
            .RuleFor(cm => cm.ChatId, f => f.Random.Int(0, 100))
            .RuleFor(cm => cm.UserId, f => f.Random.Guid().ToString())
            .RuleFor(cm => cm.Chat, f => new Chat())
            .RuleFor(cm => cm.User, f => new User());
        }

        public async Task<ChatMember?> GetEntityFromDbByUserId(DbContext dbContext, string memberId)
        {
            return await dbContext.Set<ChatMember>()
                .SingleOrDefaultAsync(a => a.UserId == memberId)
                .ConfigureAwait(false);
        }

        public async Task<List<ChatMember>> GetAllFromDb(DbContext dbContext)
        {
            return await dbContext.Set<ChatMember>()
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
