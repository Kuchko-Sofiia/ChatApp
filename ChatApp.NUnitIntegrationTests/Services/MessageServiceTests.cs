using Bogus;
using ChatApp.BLL.Services;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.UnitOfWork;
using ChatApp.DTO;
using ChatApp.NUnitIntegrationTests.TestDataProviders;

namespace ChatApp.NUnitIntegrationTests.Services
{
    [TestFixture]
    public class MessageServiceTests : IntegrationTestBase
    {
        private readonly IMessageService _messageService;
        private readonly MessageTestDataProvider _messageTestDataProvider;
        private readonly ChatTestDataProvider _chatTestDataProvider;
        private readonly UserTestDataProvider _userTestDataProvider;

        public MessageServiceTests()
        {
            _messageService = new MessageService(GetUnitOfWork());
            _messageTestDataProvider = new MessageTestDataProvider();
            _chatTestDataProvider = new ChatTestDataProvider();
            _userTestDataProvider = new UserTestDataProvider();
        }

        [Test]
        public async Task CreateMessage_WhenMessageIsValid_AddsMessageToDb()
        {
            //Arrange
            var fakeChat = await _chatTestDataProvider.InsertEntityToDb(_dbContext, _chatTestDataProvider.GenerateFakeEntity());
            var fakeSender = await _userTestDataProvider.InsertEntityToDb(_dbContext, _userTestDataProvider.GenerateFakeEntity());

            var message = _messageTestDataProvider.GenerateFakeMessageByChatId(fakeChat.Id, fakeSender.Id);

            //Act
            await _messageService.CreateMessage(message);

            var result = await _messageTestDataProvider.GetEntityFromDb(_dbContext, message.Id);

            //Assert
            Assert.That(result, Is.EqualTo(message));
        }

        [Test]
        public async Task CreateMessage_WhenMessageIsNull_ThrowArgumentNullException()
        {
            //Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _messageService.CreateMessage(null));
        }

        [Test]
        public async Task GetAllMessagesAsync_WithExistingMessages_ReturnsMessages() 
        {
            //Arrange
            var fakeChat = await _chatTestDataProvider.InsertEntityToDb(_dbContext, _chatTestDataProvider.GenerateFakeEntity());
            var fakeSender = await _userTestDataProvider.InsertEntityToDb(_dbContext, _userTestDataProvider.GenerateFakeEntity());

            var messages = _messageTestDataProvider.GenerateFakeMessagesByChatId(10, fakeChat.Id, fakeSender.Id);
            await _messageTestDataProvider.InsertRangeToDb(_dbContext, messages);

            //Act
            var result = await _messageService.GetAllMessagesAsync(fakeChat.Id);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(messages.Count));
        }

        [Test]
        public async Task GetAllMessagesAsync_WithNotExistingMessages_ReturnsEmpty()
        {
            //Arrange
            var fakeChat = await _chatTestDataProvider.InsertEntityToDb(_dbContext, _chatTestDataProvider.GenerateFakeEntity());

            //Act
            var result = await _messageService.GetAllMessagesAsync(fakeChat.Id);

            //Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        [TestCase("Hi", MessageSortProperty.SenderId, SortDirectionData.Ascending, 4, 1, 4)]
        [TestCase("you", MessageSortProperty.SentTime, SortDirectionData.Descending, 7, 1, 7)]
        [TestCase("to", MessageSortProperty.None, SortDirectionData.Ascending, 4, 1, 4)]
        [TestCase("i", MessageSortProperty.ChatId, SortDirectionData.Descending, 10, 1, 10)]
        public async Task GetPaginatedMessagesAsync_ReturnsPaginatedData(
            string searchText,
            MessageSortProperty sortProperty,
            SortDirectionData sortDirectionData,
            int expectedDataCount,
            int expectedPageIndex,
            int expectedTotalItems)
        {
            // Arrange
            DatabaseUtils.PopulateDatabase(_dbContext);

            var tableState = new PaginatedDataStateDTO<MessageSortProperty>
            {
                SearchText = searchText,
                SortProperty = sortProperty,
                SortDirection = sortDirectionData,
                PageIndex = expectedPageIndex,
                PageSize = 10
            };

            // Act
            var result = await _messageService.GetPaginatedMessagesAsync(tableState);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(expectedDataCount));
            Assert.That(result.PageIndex, Is.EqualTo(expectedPageIndex));
            Assert.That(result.TotalItems, Is.EqualTo(expectedTotalItems));
        }
    }
}
