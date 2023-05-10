using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.Repositories.Realizations;

namespace ChatApp.NUnitTests.Repositories
{
    public class AvatarRepositoryTests
    {
        private AvatarRepository _avatarRepository;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var context = await InMemoryDBContext.GetChatAppDbContext();
            _avatarRepository = new AvatarRepository(context);
        }

        [Test]
        public async Task GetByIdAsync_WithExistingAvatar_ReturnsAvatar()
        {
            var existingAvatar = new Avatar
            {
                Id = 801,
                ChatId = null,
                UserId = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                FileName = "file",
                Content = "avatar801",
                ContentType = "jpg"
            };
            _avatarRepository.Create(existingAvatar);

            var result = await _avatarRepository.GetById(existingAvatar.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(existingAvatar));
        }

        [Test]
        [TestCase(3523)]
        [TestCase(-3523)]
        [TestCase(0)]
        [TestCase(623)]
        public async Task GetByIdAsync_WithNonExistingAvatar_ReturnsNull(int nonExistingAvatarId)
        {
            var result = await _avatarRepository.GetById(nonExistingAvatarId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddAsync_AddsAvatarToRepository()
        {
            var newAvatar = new Avatar
            {
                Id = 802,
                ChatId = null,
                UserId = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                FileName = "file",
                Content = "avatar802",
                ContentType = "jpg"
            };

            _avatarRepository.Create(newAvatar);
            var result = await _avatarRepository.GetById(newAvatar.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(newAvatar));
        }

        [Test]
        public async Task DeleteAsync_DeletesChatFromRepository()
        {
            var avatarToDelete = new Avatar
            {
                Id = 803,
                ChatId = null,
                UserId = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                FileName = "file",
                Content = "avatar803",
                ContentType = "jpg"
            };

            _avatarRepository.Create(avatarToDelete);

            _avatarRepository.Delete(avatarToDelete);
            var result = await _avatarRepository.GetById(avatarToDelete.Id);

            Assert.That(result, Is.Null);
        }
    }
}
