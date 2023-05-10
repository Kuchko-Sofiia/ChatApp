using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.Repositories.Realizations;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.NUnitTests.Repositories
{
    public class UserRepositoryTests
    {
        private UserRepository _userRepository;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var context = await InMemoryDBContext.GetChatAppDbContext();
            _userRepository = new UserRepository(context);
        }

        [Test]
        public async Task GetByIdAsync_WithExistingUser_ReturnsUser()
        {
            var existingUser = new User
            {
                Id = "111",
                UserName = "user111",
                Email = "user111@example.com",
                FirstName = "John111",
                LastName = "Doe111",
                PhoneNumber = "+380734636585",
            };
            _userRepository.Create(existingUser);

            var result = await _userRepository.GetById("111");

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(existingUser));
        }

        [Test]
        public async Task GetByIdAsync_WithNonExistingUser_ReturnsNull()
        {
            var nonExistingUserId = "1";

            var result = await _userRepository.GetById(nonExistingUserId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllUsers()
        {
            var result = await _userRepository.GetAllAsync();

            Assert.That(result.Count, Is.EqualTo(InMemoryDBContext.FakeUsers.Count));
        }

        [Test]
        public async Task AddAsync_AddsUserToRepository()
        {
            var newUser = new User
            {
                Id = "222",
                UserName = "user222",
                Email = "user222@example.com",
                FirstName = "John222",
                LastName = "Doe222",
                PhoneNumber = "+380734636585",
            };

            _userRepository.Create(newUser);
            var result = await _userRepository.GetById("222");

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(newUser));
        }

        [Test]
        public async Task DeleteAsync_DeletesUserFromRepository()
        {
            var existingUser = new User
            {
                Id = "333",
                UserName = "user333",
                Email = "user333@example.com",
                FirstName = "John333",
                LastName = "Doe333",
                PhoneNumber = "+380734636585",
            };

            _userRepository.Create(existingUser);

            _userRepository.Delete(existingUser);
            var result = await _userRepository.GetById("333");

            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase("02889dcc-31a8-4457-a08b-8ae2c0ffdec6", 2)]
        [TestCase("05786c29-074c-4499-ab29-21b6669c4f2b", 1)]
        [TestCase("0782e029-6e0f-4dde-98d3-02b11c0cd088", 1)]
        [TestCase("153f78cf-fa5b-482f-98da-bf912c19869e", 0)]
        public async Task GetUserWithAvatarById_ReturnsCorrectUser(string userId, int avatarsCount)
        {
            var result = await _userRepository.GetUserWithAvatarById(userId);

            Assert.That(result.Avatars, Has.Count.EqualTo(avatarsCount));
        }

        [Test]
        public void GetAllUsersWithAvatars_ReturnsAllUsersWithAvatars()
        {
            var result = _userRepository.GetAllUsersWithAvatars().ToList();

            Assert.That(result, Is.Not.Null);

            foreach(var user in result)
            {
                Assert.That(user.Avatars, Is.Not.Null);
            }
        }
    }
}
