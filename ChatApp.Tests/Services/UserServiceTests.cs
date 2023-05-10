using ChatApp.BLL.Services;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.UnitOfWork;
using ChatApp.DTO;

namespace ChatApp.NUnitTests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IAvatarRepository> _avatarRepositoryMock;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _avatarRepositoryMock = new Mock<IAvatarRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.GetRepository<IUserRepository>()).Returns(_userRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<IAvatarRepository>()).Returns(_avatarRepositoryMock.Object);

            _userService = new UserService(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task GetUserById_ReturnsUserWithAvatar()
        {
            string userId = "123";
            var expectedUser = new User { Id = userId, Avatars = new List<Avatar> { new Avatar() } };
            _userRepositoryMock.Setup(repo => repo.GetUserWithAvatarById(userId)).ReturnsAsync(expectedUser);

            var result = await _userService.GetUserById(userId);

            Assert.That(result, Is.EqualTo(expectedUser));
        }

        [Test]
        public async Task GetUsersAsync_ReturnsPaginatedDataOfUsers()
        {
            var tableState = new PaginatedDataStateDTO<UserInfoSortProperty> { PageIndex = 1, PageSize = 10 };

            var users = new List<User> { new User { Id = "1" }, new User { Id = "2" } }.AsQueryable();
            var mock = users.AsQueryable().BuildMock();

            _userRepositoryMock.Setup(repo => repo.GetAll()).Returns(mock);

            var result = await _userService.GetUsersAsync(tableState);

            Assert.That(result.PageIndex, Is.EqualTo(1));
        }

        [Test]
        public async Task EditUser_UpdatesUserAndSavesChanges()
        {
            var userToEdit = new UserDTO { Id = "123", UserName = "new_username" };
            var user = new User { Id = userToEdit.Id, UserName = "old_username" };
            _userRepositoryMock.Setup(repo => repo.GetById(userToEdit.Id)).ReturnsAsync(user);

            var result = await _userService.EditUser(userToEdit);

            Assert.IsTrue(result);
            Assert.That(user.UserName, Is.EqualTo(userToEdit.UserName));

            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task AddAvatarAsync_CreatesAvatarAndSavesChanges()
        {
            var avatar = new Avatar();

            await _userService.AddAvatarAsync(avatar);

            _avatarRepositoryMock.Verify(repo => repo.Create(avatar), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task RemoveAvatarAsync_DeletesAvatarAndSavesChanges()
        {
            int avatarId = 123;
            var avatarToDelete = new Avatar { Id = avatarId };
            _avatarRepositoryMock.Setup(repo => repo.GetById(avatarId)).ReturnsAsync(avatarToDelete);

            await _userService.RemoveAvatarAsync(avatarId);

            _avatarRepositoryMock.Verify(repo => repo.Delete(avatarToDelete), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }
    }
}