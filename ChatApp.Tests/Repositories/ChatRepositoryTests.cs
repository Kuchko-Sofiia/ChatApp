using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.Repositories.Realizations;

namespace ChatApp.NUnitTests.Repositories
{
    public class ChatRepositoryTests
    {
        private ChatRepository _chatRepository;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var context = await InMemoryDBContext.GetChatAppDbContext();
            _chatRepository = new ChatRepository(context);
        }

        [Test]
        public async Task GetByIdAsync_WithExistingChat_ReturnsChat()
        {
            var existingChat = new Chat
            {
                Id = 958,
                Name = "General",
                Description = "General chat for everyone",
                MembersCount = 10
            };

            _chatRepository.Create(existingChat);

            var result = await _chatRepository.GetById(existingChat.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(existingChat));
        }

        [Test]
        [TestCase(1523)]
        [TestCase(-1523)]
        [TestCase(9973)]
        [TestCase(223)]
        public async Task GetByIdAsync_WithNonExistingChat_ReturnsNull(int nonExistingChatId)
        {
            var result = await _chatRepository.GetById(nonExistingChatId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddAsync_AddsChatToRepository()
        {
            var newChat = new Chat
            {
                Id = 700,
                Name = "General1",
                Description = "General1 chat for everyone",
                MembersCount = 9
            };

            _chatRepository.Create(newChat);
            var result = await _chatRepository.GetById(newChat.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(newChat));
        }

        [Test]
        public async Task DeleteAsync_DeletesChatFromRepository()
        {
            var chatToDelete = new Chat
            {
                Id = 701,
                Name = "General2",
                Description = "General2 chat for everyone",
                MembersCount = 8
            };

            _chatRepository.Create(chatToDelete);

            _chatRepository.Delete(chatToDelete);
            var result = await _chatRepository.GetById(chatToDelete.Id);

            Assert.That(result, Is.Null);
        }
    }
}
