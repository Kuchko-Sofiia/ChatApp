using Bogus;
using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.NUnitIntegrationTests.TestDataProviders
{
    public class MessageTestDataProvider : TestDataProvider<Message>
    {
        protected override void ConfigureFaker(Faker<Message> faker)
        {
            faker.RuleFor(m => m.Text, f => f.Lorem.Sentence())
                 .RuleFor(m => m.ChatId, f => f.Random.Int(1, 100))
                 .RuleFor(m => m.SenderId, f => f.Random.Bool() ? f.Random.Guid().ToString() : null)
                 .RuleFor(m => m.SentTime, f => f.Date.Past(30))
                 .RuleFor(m => m.MessageStatus, f => f.Random.Int(0, 2));
        }

        public List<Message> GenerateFakeMessagesByChatId(int count, int chatId, string senderId)
        {
            ConfigureFaker(_faker);

            _faker.RuleFor(a => a.ChatId, chatId);
            _faker.RuleFor(a => a.SenderId, senderId);

            return _faker.Generate(count);
        }

        public Message GenerateFakeMessageByChatId(int chatId, string senderId)
        {
            ConfigureFaker(_faker);

            _faker.RuleFor(a => a.ChatId, chatId);
            _faker.RuleFor(a => a.SenderId, senderId);

            return _faker.Generate();
        }

        public async Task<Message?> GetEntityFromDb(DbContext dbContext, int id)
        {
            return await dbContext.Set<Message>()
                .SingleOrDefaultAsync(a => a.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<Message> InsertEntityToDb(DbContext dbContext, Message entity)
        {
            dbContext.Add(entity);
            await dbContext.SaveChangesAsync();

            return await dbContext.Set<Message>().FirstAsync(x => x.Id == entity.Id);
        }

        public async Task InsertRangeToDb(DbContext dbContext, IEnumerable<Message> entities)
        {
            await dbContext.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }
    }
}
