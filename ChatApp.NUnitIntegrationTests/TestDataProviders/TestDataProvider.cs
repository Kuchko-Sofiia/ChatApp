using Bogus;
using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.NUnitIntegrationTests.TestDataProviders
{
    public abstract class TestDataProvider<T> where T : class
    {
        private protected static readonly Faker<T> _faker = new Faker<T>();

        protected abstract void ConfigureFaker(Faker<T> faker);

        public virtual T GenerateFakeEntity()
        {
            ConfigureFaker(_faker);
            return _faker.Generate();
        }

        public virtual List<T> GenerateFakeEntities(int count)
        {
            ConfigureFaker(_faker);
            return _faker.Generate(count);
        }
    }
}
