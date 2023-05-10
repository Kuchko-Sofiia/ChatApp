using ChatApp.DAL.Repositories.Realizations;

namespace ChatApp.NUnitTests.Repositories
{
    public class ChatInfoRepositoryTests
    {
        private ChatInfoRepository _chatInfoRepository;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var context = await InMemoryDBContext.GetChatAppDbContext();
            _chatInfoRepository = new ChatInfoRepository(context);
        }
    }
}
