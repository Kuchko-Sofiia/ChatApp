using Bogus;
using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.NUnitIntegrationTests.TestDataProviders
{
    public class ChatTestDataProvider : TestDataProvider<Chat>
    {
        protected override void ConfigureFaker(Faker<Chat> faker)
        {
            faker.RuleFor(c => c.Name, f => f.Lorem.Word())
            .RuleFor(c => c.Description, f => f.Random.Bool() ? f.Lorem.Sentence() : null)
            .RuleFor(c => c.MembersCount, f => f.Random.Int(0, 100))
            .RuleFor(c => c.ChatMembers, f => new List<ChatMember>())
            .RuleFor(c => c.Avatars, f => new List<Avatar>());
        }

        public async Task<Chat?> GetEntityFromDb(DbContext dbContext, int id)
        {
            return await dbContext.Set<Chat>()
                .SingleOrDefaultAsync(a => a.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<List<Chat>> GetAllFromDb(DbContext dbContext)
        {
            return await dbContext.Set<Chat>()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Chat> InsertEntityToDb(DbContext dbContext, Chat entity)
        {
            dbContext.Add(entity);
            await dbContext.SaveChangesAsync();

            return await dbContext.Set<Chat>().FirstAsync(x => x.Id == entity.Id);
        }

        public async Task InsertRangeToDb(DbContext dbContext, IEnumerable<Chat> entities)
        {
            await dbContext.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Chat> UpdateEntity(DbContext dbContext, Chat entity)
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();

            return await dbContext.Set<Chat>().FirstAsync(x => x.Id == entity.Id);
        }
    }
}
