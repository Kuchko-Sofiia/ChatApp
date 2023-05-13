using ChatApp.BLL.Services;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.DTO;
using ChatApp.NUnitIntegrationTests.TestDataProviders;

namespace ChatApp.NUnitIntegrationTests.Services
{
    [TestFixture]
    public class UserServiceTests : IntegrationTestBase
    {
        private readonly IUserService _userService;
        private readonly UserTestDataProvider _userTestDataProvider;
        private readonly AvatarTestDataProvider _avatarTestDataProvider;

        public UserServiceTests()
        {
            _userService = new UserService(GetUnitOfWork());
            _userTestDataProvider = new UserTestDataProvider();
            _avatarTestDataProvider = new AvatarTestDataProvider();
        }

        [Test]
        public async Task GetUserByIdAsync_WhenUserIdExists_ReturnsUser()
        {
            //Arrange
    
            var newUser = await _userTestDataProvider.InsertEntityToDb(_dbContext, _userTestDataProvider.GenerateFakeEntity());

            //Act
            var result = await _userService.GetUserById(newUser.Id);

            //Assert
            Assert.That(newUser, Is.EqualTo(result));
        }

        [Test]
        public async Task GetUserByIdAsync_WhenUserIdNotExists_ReturnsNull()
        {
            //Arrange
            await _userTestDataProvider.InsertEntityToDb(_dbContext, _userTestDataProvider.GenerateFakeEntity());

            //Act
            var result = await _userService.GetUserById("fakeId");

            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task EditUser_WhenUserExists_ReturnsTrue()
        {
            // Arrange
            var newUser = await _userTestDataProvider.InsertEntityToDb(_dbContext, _userTestDataProvider.GenerateFakeEntity());

            var userToEdit = new UserDTO
            {
                Id = newUser.Id,
                UserName = "newUserName",
                Email = "newEmail@example.com",
                FirstName = "newFirstName",
                LastName = "newLastName",
                PhoneNumber = "newPhoneNumber"
            };

            // Act
            var result = await _userService.EditUser(userToEdit);

            // Assert
            Assert.That(result, Is.True);
            var updatedUser = await _userTestDataProvider.GetEntityFromDb(_dbContext, newUser.Id);

            Assert.That(updatedUser, Is.Not.Null);
            Assert.That(updatedUser.UserName, Is.EqualTo(userToEdit.UserName));
            Assert.That(updatedUser.Email, Is.EqualTo(userToEdit.Email));
            Assert.That(updatedUser.FirstName, Is.EqualTo(userToEdit.FirstName));
            Assert.That(updatedUser.LastName, Is.EqualTo(userToEdit.LastName));
            Assert.That(updatedUser.PhoneNumber, Is.EqualTo(userToEdit.PhoneNumber));
        }

        [Test]
        public async Task EditUser_WhenUserNotExists_ReturnsFalse()
        {
            // Arrange
            var userToEdit = new UserDTO
            {
                Id = "randomId",
                UserName = "newUserName",
                Email = "newEmail@example.com",
                FirstName = "newFirstName",
                LastName = "newLastName",
                PhoneNumber = "newPhoneNumber"
            };

            // Act
            var result = await _userService.EditUser(userToEdit);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task AddAvatarAsync_AddsAvatar()
        {
            //Arrange
            var newUser = await _userTestDataProvider.InsertEntityToDb(_dbContext, _userTestDataProvider.GenerateFakeEntity());
            var newAvatar = _avatarTestDataProvider.GenerateFakeEntity(newUser.Id);

            //Act
            await _userService.AddAvatarAsync(newAvatar);

            var result = await _avatarTestDataProvider.GetEntityFromDb(_dbContext, newAvatar.Id);

            //Assert
            Assert.That(result, Is.EqualTo(newAvatar));
        }

        [Test]
        public async Task RemoveAvatarByIdAsync_RemovesAvatar()
        {
            //Arrange
            var newUser = await _userTestDataProvider.InsertEntityToDb(_dbContext, _userTestDataProvider.GenerateFakeEntity());

            var newAvatar = await _avatarTestDataProvider.InsertEntityToDb(_dbContext, _avatarTestDataProvider.GenerateFakeEntity(newUser.Id));

            //Act
            await _userService.RemoveAvatarAsync(newAvatar.Id);

            var result = await _avatarTestDataProvider.GetEntityFromDb(_dbContext, newAvatar.Id);
            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase("user1", UserInfoSortProperty.FirstName, SortDirectionData.Ascending, 7, 1, 7)]
        [TestCase("notexistingsearchtext", UserInfoSortProperty.LastName, SortDirectionData.Descending, 0, 1, 0)]
        [TestCase("Merry", UserInfoSortProperty.None, SortDirectionData.Ascending, 1, 1, 1)]
        [TestCase("Doe", UserInfoSortProperty.Email, SortDirectionData.Descending, 2, 1, 2)]
        [TestCase("073", UserInfoSortProperty.UserName, SortDirectionData.None, 4, 1, 4)]
        public async Task GetUsersAsync_ReturnsPaginatedData(
            string searchText,
            UserInfoSortProperty sortProperty,
            SortDirectionData sortDirectionData,
            int expectedDataCount,
            int expectedPageIndex,
            int expectedTotalItems)
        {
            // Arrange
            DatabaseUtils.PopulateDatabase(_dbContext);

            var tableState = new PaginatedDataStateDTO<UserInfoSortProperty>
            {
                SearchText = searchText,
                SortProperty = sortProperty,
                SortDirection = sortDirectionData,
                PageIndex = expectedPageIndex,
                PageSize = 10
            };

            // Act
            var result = await _userService.GetUsersAsync(tableState);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(expectedDataCount));
            Assert.That(result.PageIndex, Is.EqualTo(expectedPageIndex));
            Assert.That(result.TotalItems, Is.EqualTo(expectedTotalItems));
        }
    }
}
