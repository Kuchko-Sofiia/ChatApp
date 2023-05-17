using Bogus;
using ChatApp.BLL.Services;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.DAL.Entities;
using ChatApp.DTO;
using ChatApp.NUnitIntegrationTests.TestDataProviders;

namespace ChatApp.NUnitIntegrationTests.Services
{
    [TestFixture]
    public class ChatServiceTests : IntegrationTestBase
    {
        private readonly IChatService _chatService;
        private readonly ChatTestDataProvider _chatTestDataProvider;
        private readonly UserTestDataProvider _userTestDataProvider;
        private readonly ChatMemberTestDataProvider _chatMemberTestDataProvider;

        public ChatServiceTests()
        {
            _chatService = new ChatService(GetUnitOfWork());
            _chatTestDataProvider = new ChatTestDataProvider();
            _userTestDataProvider = new UserTestDataProvider();
            _chatMemberTestDataProvider = new ChatMemberTestDataProvider();
        }

        [Test]
        public async Task CreateChat_WhenChatExists_AddsChatAndMembersToDb()
        {
            //Arrange
            var fakeUsers = _userTestDataProvider.GenerateFakeEntities(10);
            await _userTestDataProvider.InsertRangeToDb(_dbContext, fakeUsers);
            var fakeUsersId = new List<string>();
            foreach(var user in fakeUsers)
            {
                fakeUsersId.Add(user.Id);
            }
            var newChat = _chatTestDataProvider.GenerateFakeEntity();

            //Act
            await _chatService.CreateChat(newChat, fakeUsersId);

            var chatResult = await _chatTestDataProvider.GetEntityFromDb(_dbContext, newChat.Id);
            var chatMembersResult = await _chatMemberTestDataProvider.GetAllFromDb(_dbContext);

            //Assert
            Assert.That(chatResult, Is.Not.Null);
            Assert.That(chatMembersResult, Is.Not.Null);
            Assert.That(chatResult, Is.EqualTo(newChat));
            Assert.That(chatMembersResult.Count, Is.EqualTo(fakeUsers.Count));
        }

        [Test]
        public async Task CreateChat_WhenChatIsNull_ThrowArgumentNullException()
        {
            //Arrange
            var fakeUsers = _userTestDataProvider.GenerateFakeEntities(3);
            await _userTestDataProvider.InsertRangeToDb(_dbContext, fakeUsers);
            var fakeUsersId = new List<string>();
            foreach (var user in fakeUsers)
            {
                fakeUsersId.Add(user.Id);
            }

            //Act|Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _chatService.CreateChat(null, fakeUsersId));
        }

        [Test]
        public async Task CreateChat_WhenFakeUsersIdIsNull_ThrowArgumentNullException()
        {
            //Arrange
            var newChat = _chatTestDataProvider.GenerateFakeEntity();

            //Act|Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _chatService.CreateChat(newChat, null));
        }

        [Test]
        public async Task CreateChat_WhenFakeUsersIdIsEmpty_ThrowArgumentException()
        {
            //Arrange
            var fakeUsersId = new List<string>();
            var newChat = _chatTestDataProvider.GenerateFakeEntity();

            //Act|Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _chatService.CreateChat(newChat, fakeUsersId));
        }

        [Test]
        public async Task GetChatByIdAsync_WhenChatIdExists_ReturnsChat()
        {
            //Arrange
            var fakeUsers = _userTestDataProvider.GenerateFakeEntities(3);
            await _userTestDataProvider.InsertRangeToDb(_dbContext, fakeUsers);
            var fakeUsersId = new List<string>();
            foreach (var user in fakeUsers)
            {
                fakeUsersId.Add(user.Id);
            }

            var newChat = await _chatTestDataProvider.InsertEntityToDb(_dbContext, _chatTestDataProvider.GenerateFakeEntity());

            //Act
            var result = await _chatService.GetChatById(newChat.Id);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(newChat));
        }

        [Test]
        [TestCase(98357)]
        public async Task GetChatByIdAsync_WhenChatIdNotExists_ReturnsNull(int notExistedChatId)
        {
            //Act
            var result = await _chatService.GetChatById(notExistedChatId);

            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase("ChatDoesntExist", ChatSortProperty.Name, SortDirectionData.Ascending, 0, 1, 0)]
        [TestCase("General", ChatSortProperty.MembersCount, SortDirectionData.Descending, 1, 1, 1)]
        [TestCase("chat", ChatSortProperty.Description, SortDirectionData.Ascending, 6, 1, 6)]
        [TestCase("department", ChatSortProperty.None, SortDirectionData.Descending, 4, 1, 4)]
        [TestCase("in", ChatSortProperty.Name, SortDirectionData.None, 2, 1, 2)]
        public async Task GetPaginatedChatsAsync_ReturnsPaginatedData(
            string searchText,
            ChatSortProperty sortProperty,
            SortDirectionData sortDirectionData,
            int expectedDataCount,
            int expectedPageIndex, 
            int expectedTotalItems)
        {
            // Arrange

            var tableState = new PaginatedDataStateDTO<ChatSortProperty>
            {
                SearchText = searchText,
                SortProperty = sortProperty,
                SortDirection = sortDirectionData,
                PageIndex = expectedPageIndex,
                PageSize = 10
            };

            // Act
                var result = await _chatService.GetPaginatedChatsAsync(tableState);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(expectedDataCount));
            Assert.That(result.PageIndex, Is.EqualTo(expectedPageIndex));
            Assert.That(result.TotalItems, Is.EqualTo(expectedTotalItems));
        }

        [Test]
        [TestCase("ChatDoesntExist", ChatSortProperty.Name, SortDirectionData.Ascending, 0, 1, 0, "02889dcc-31a8-4457-a08b-8ae2c0ffdec6")]
        [TestCase("General", ChatSortProperty.Description, SortDirectionData.Ascending, 1, 1, 1, "02889dcc-31a8-4457-a08b-8ae2c0ffdec6")]
        [TestCase("chat", ChatSortProperty.None, SortDirectionData.Descending, 1, 1, 1, "0782e029-6e0f-4dde-98d3-02b11c0cd088")]
        public async Task GetPaginatedChatsByUserIdAsync_ReturnsPaginatedData(
            string searchText,
            ChatSortProperty sortProperty,
            SortDirectionData sortDirectionData,
            int expectedDataCount,
            int expectedPageIndex,
            int expectedTotalItems,
            string userId)
        {
            // Arrange

            var tableState = new PaginatedDataStateDTO<ChatSortProperty>
            {
                SearchText = searchText,
                SortProperty = sortProperty,
                SortDirection = sortDirectionData,
                PageIndex = expectedPageIndex,
                PageSize = 10
            };

            // Act
            var result = await _chatService.GetPaginatedChatsByUserIdAsync(tableState, userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(expectedDataCount));
            Assert.That(result.PageIndex, Is.EqualTo(expectedPageIndex));
            Assert.That(result.TotalItems, Is.EqualTo(expectedTotalItems));
        }
    }
}
