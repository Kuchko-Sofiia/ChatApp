using Bogus;
using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.NUnitIntegrationTests.TestDataProviders
{
    public class UserTestDataProvider : TestDataProvider<User>
    {
        protected override void ConfigureFaker(Faker<User> faker)
        {
            faker.RuleFor(x => x.Id, _ => Guid.NewGuid().ToString())
                 .RuleFor(x => x.UserName, x => x.Person.UserName)
                 .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                 .RuleFor(x => x.LastName, x => x.Person.LastName)
                 .RuleFor(x => x.Email, x => x.Person.Email)
                 .RuleFor(x => x.PhoneNumber, x => x.Person.Phone)
                 .RuleFor(x => x.DateOfBirth, _ => DateTime.Today.AddDays(Random.Shared.Next(-230, -8)));
        }

        public async Task<User?> GetEntityFromDb(DbContext dbContext, string id)
        {
            return await dbContext.Set<User>()
                .SingleOrDefaultAsync(a => a.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<User> InsertEntityToDb(DbContext dbContext, User entity)
        {
            dbContext.Add(entity);
            await dbContext.SaveChangesAsync();

            return await dbContext.Set<User>().FirstAsync(x => x.Id == entity.Id);
        }

        public async Task InsertRangeToDb(DbContext dbContext, IEnumerable<User> entities)
        {
            await dbContext.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task<User> UpdateEntity(DbContext dbContext, User entity)
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();

            return await dbContext.Set<User>().FirstAsync(x => x.Id == entity.Id);
        }
    }
}
