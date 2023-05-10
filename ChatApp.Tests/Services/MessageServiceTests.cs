using ChatApp.BLL.Services;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.UnitOfWork;
using ChatApp.DTO;

namespace ChatApp.NUnitTests.Services
{
    [TestFixture]
    public class MessageServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMessageRepository> _messageRepositoryMock;
        private MessageService _messageService;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _messageRepositoryMock = new Mock<IMessageRepository>();
            _messageService = new MessageService(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task CreateMessage_Should_CreateMessage_And_SaveChangesAsync()
        {
            var newMessage = new Message();
            _unitOfWorkMock.Setup(u => u.GetRepository<IMessageRepository>()).Returns(_messageRepositoryMock.Object);

            await _messageService.CreateMessage(newMessage);

            _messageRepositoryMock.Verify(m => m.Create(newMessage), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllMessagesAsync_Should_Return_Messages_By_ChatId()
        {
            int chatId = 1;
            var messages = new List<Message>() { new Message(), new Message() };
            var mock = messages.AsQueryable().BuildMock();

            _unitOfWorkMock.Setup(u => u.GetRepository<IMessageRepository>()).Returns(_messageRepositoryMock.Object);
            _messageRepositoryMock.Setup(m => m.GetAllByChatId(chatId)).Returns(mock);

            var result = await _messageService.GetAllMessagesAsync(chatId);

            Assert.That(result, Is.EqualTo(messages));
            _messageRepositoryMock.Verify(m => m.GetAllByChatId(chatId), Times.Once);
        }

        [Test]
        public async Task GetPaginatedMessagesAsync_Should_Return_Paginated_And_Sorted_Messages()
        {
            var messages = new List<Message>()
            {
                new Message() { ChatId = 1, SenderId = "1", SentTime = DateTime.Now },
                new Message() { ChatId = 2, SenderId = "2", SentTime = DateTime.Now.AddDays(1) },
                new Message() { ChatId = 3, SenderId = "3", SentTime = DateTime.Now.AddDays(2) }
            };
            var mock = messages.AsQueryable().BuildMock();

            var paginatedDataState = new PaginatedDataStateDTO<MessageSortProperty>()
            {
                PageIndex = 1,
                PageSize = 1,
                SortDirection = SortDirectionData.Ascending,
                SortProperty = MessageSortProperty.ChatId
            };
            _unitOfWorkMock.Setup(u => u.GetRepository<IMessageRepository>()).Returns(_messageRepositoryMock.Object);
            _messageRepositoryMock.Setup(m => m.GetAll()).Returns(mock);

            var result = await _messageService.GetPaginatedMessagesAsync(paginatedDataState);

            Assert.Multiple(() =>
            {
                Assert.That(result.TotalItems, Is.EqualTo(3));
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result.TotalPages, Is.EqualTo(3));
                Assert.That(result, Is.EquivalentTo(messages.OrderBy(m => m.ChatId).Take(1)));
            });
            _messageRepositoryMock.Verify(m => m.GetAll(), Times.Once);
        }

        [Test]
        public Task CreateMessage_Should_Throw_Exception_If_Message_Is_Null()
        {
            Message newMessage = null;
            _unitOfWorkMock.Setup(u => u.GetRepository<IMessageRepository>()).Returns(_messageRepositoryMock.Object);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await _messageService.CreateMessage(newMessage));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null. (Parameter 'newMessage')"));
            _messageRepositoryMock.Verify(m => m.Create(newMessage), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
            return Task.CompletedTask;
        }
    }
}
