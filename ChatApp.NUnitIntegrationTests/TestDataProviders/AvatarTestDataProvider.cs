using Bogus;
using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.NUnitIntegrationTests.TestDataProviders
{
    public class AvatarTestDataProvider : TestDataProvider<Avatar>
    {
        protected override void ConfigureFaker(Faker<Avatar> faker)
        {
            faker.RuleFor(a => a.Id, f => 0)
            .RuleFor(a => a.FileName, f => f.System.FileName())
            .RuleFor(a => a.ContentType, f => f.System.MimeType())
            .RuleFor(a => a.Content, f => Convert.ToBase64String(f.Random.Bytes(100)))
            .RuleFor(a => a.UserId, f => f.Random.Bool() ? f.Random.Guid().ToString() : null)
            .RuleFor(a => a.ChatId, f => f.Random.Bool() ? f.Random.Int(1, 100) : null);
        }
        public Avatar GenerateFakeEntity(string userId, int? chatId = null)
        {
            ConfigureFaker(_faker);

            if (!string.IsNullOrEmpty(userId))
            {
                _faker.RuleFor(a => a.UserId, userId)
                    .RuleFor(a => a.ChatId, (int?)null);
            }
            else if (chatId.HasValue)
            {
                _faker.RuleFor(a => a.ChatId, chatId)
                    .RuleFor(a => a.UserId, (string)null);
            }

            return _faker.Generate();
        }

        public async Task<Avatar?> GetEntityFromDb(DbContext dbContext, int id)
        {
            return await dbContext.Set<Avatar>()
                .SingleOrDefaultAsync(a => a.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<Avatar> InsertEntityToDb(DbContext dbContext, Avatar entity)
        {
            dbContext.Set<Avatar>().Add(entity);
            dbContext.SaveChanges();

            return await dbContext.Set<Avatar>().FirstAsync(x => x.Id == entity.Id);
        }

        public async Task<Avatar> UpdateEntity(DbContext dbContext, Avatar entity)
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();

            return await dbContext.Set<Avatar>().FirstAsync(x => x.Id == entity.Id);
        }
    }
}
