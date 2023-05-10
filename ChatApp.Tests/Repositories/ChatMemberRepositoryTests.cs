using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Realizations;

namespace ChatApp.NUnitTests.Repositories
{
    public class ChatMemberRepositoryTests
    {
        private ChatMemberRepository _chatMemberRepository;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var context = await InMemoryDBContext.GetChatAppDbContext();
            _chatMemberRepository = new ChatMemberRepository(context);
        }

        [Test]
        [TestCase("user1", 2)]
        [TestCase("user2", 2)]
        [TestCase("user3", 1)]
        public void GetUsersChats_ReturnsChatsForUserId(string userId, int expectedChatsCount)
        {
            IQueryable<Chat> result = _chatMemberRepository.GetUsersChats(userId);

            Assert.That(result.Count(), Is.EqualTo(expectedChatsCount));
        }

        [Test]
        [TestCase("user99", 0)]
        [TestCase("user9w9", 0)]
        [TestCase("sjksk", 0)]
        public void GetUsersChats_ReturnsEmptyListForUnknownUserId(string userId, int expectedChatsCount)
        {
            IQueryable<Chat> result = _chatMemberRepository.GetUsersChats(userId);

            Assert.That(result, Is.Empty);
            Assert.That(result.Count(), Is.EqualTo(expectedChatsCount));
        }
    }
}
