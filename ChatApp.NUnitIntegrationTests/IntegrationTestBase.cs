using ChatApp.BLL.Services;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.DAL.Data;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.Repositories.Realizations;
using ChatApp.DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.NUnitIntegrationTests
{
    [TestFixture]
    public class IntegrationTestBase
    {
        private const string LOCALDB_CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Database=IntegrationTestChatAppDB;Trusted_Connection=True; MultipleActiveResultSets=true; TrustServerCertificate=True";

        protected IServiceProvider _serviceProvider;
        protected UserManager<User> _userManager;
        protected DbContext _dbContext;

        [SetUp]
        public async Task SetUp()
        {
            await DatabaseUtils.ClearDatabase(_dbContext);
            await DatabaseUtils.ResetIdentityForAllTables(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseUtils.DetachAllEntities(_dbContext);
        }

        protected IntegrationTestBase()
        {
            var serviceCollection = new ServiceCollection()
                .AddDbContext<ChatAppDbContext>(opt => opt.UseSqlServer(LOCALDB_CONNECTION_STRING).EnableSensitiveDataLogging())
                .AddDbContext<IdentityDbContext>(opt => opt.UseSqlServer(LOCALDB_CONNECTION_STRING).EnableSensitiveDataLogging())
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IChatRepository, ChatRepository>()
                .AddScoped<IMessageRepository, MessageRepository>()
                .AddScoped<IChatMemberRepository, ChatMemberRepository>()
                .AddScoped<IChatInfoRepository, ChatInfoRepository>()
                .AddScoped<IAvatarRepository, AvatarRepository>()
                .AddScoped<IJwtTokenService, JwtTokenService>()
                .AddScoped<IUnitOfWork, UnitOfWork>();

            serviceCollection.AddIdentityCore<User>()
                .AddEntityFrameworkStores<IdentityDbContext>();

            _serviceProvider = serviceCollection.BuildServiceProvider();

            _dbContext = _serviceProvider.GetService<ChatAppDbContext>()
                              ?? throw new InvalidOperationException("Failed to retrieve ChatAppDbContext instance from service provider.");
            _userManager = _serviceProvider.GetService<UserManager<User>>()
                              ?? throw new InvalidOperationException("Failed to retrieve UserManager<User> instance from service provider.");
        }

        protected IUnitOfWork GetUnitOfWork()
        {
            return _serviceProvider.GetService<IUnitOfWork>()
                         ?? throw new InvalidOperationException("Failed to retrieve IUnitOfWork instance from service provider.");
        }
    }
}
